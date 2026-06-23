import { useEffect, useState } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardHeader, ListGroup, ListGroupItem, Table } from "reactstrap";
import { getOrder, deleteOrder } from "../../managers/orderManager";
import { deletePizza } from "../../managers/pizzaManager";


export default function OrderDetail({ loggedInUser }) {
  const { id } = useParams();
  const navigate = useNavigate();
  const [order, setOrder] = useState(null);

  useEffect(() => {
    getOrder(id).then(setOrder);
  }, [id]);

  const refreshOrder = () => getOrder(id).then(setOrder);

  if (!order) return <p>Loading...</p>;

  return (
    <div className="container mt-4">
      <h2>
        Order #{order.id}
      </h2>
        {loggedInUser?.roles?.includes("Admin") && (
            <>
                <Link to={`/orders/${order.id}/pizzas/create`}>
                <Button color="primary" className="mb-3">Add Pizza</Button>
                </Link>
                {" "}
                <Link to={`/orders/${order.id}/edit`}>
                <Button color="secondary" className="mb-3">Edit Order</Button>
                </Link>
            </>
        )}
      <Card className="mb-4">
        <CardHeader>Order Details</CardHeader>
        <ListGroup flush>
            {order.isDelivery ? 
            <ListGroupItem>
                <strong>Deliverer:</strong> {order.deliverer.firstName} {order.deliverer.lastName}
            </ListGroupItem> :
            <ListGroupItem>
                <strong>Table Number:</strong> {order.tableNumber}
            </ListGroupItem>
            }
            <ListGroupItem>
                <strong>Order Date:</strong> {new Date(order.orderDate).toLocaleString()}
            </ListGroupItem>
            <ListGroupItem>
                <strong>Employee:</strong> {order.employee.firstName} {order.employee.lastName}
            </ListGroupItem>
            <ListGroupItem>
                <strong>Total:</strong> ${order.total.toFixed(2)}
            </ListGroupItem>
            <ListGroupItem>
                <strong>Tip:</strong> {order.tip ? `$${order.tip.toFixed(2)}` : "No tip"}
            </ListGroupItem>
        </ListGroup>
      </Card>

      <Card className="mb-4">
        <CardHeader>Pizzas</CardHeader>
        <CardBody>
            {order.pizzas?.length > 0 ? (
            <Table striped className="mt-3">
            <thead>
                <tr>
                <th>Size</th>
                <th>Cheese</th>
                <th>Sauce</th>
                <th>Toppings</th>
                <th>Total</th>
                <th></th>
                </tr>
            </thead>
            <tbody>
                {order.pizzas.map((p) => (
                <tr key={p.id}>
                    <td>{p.size.name}</td>
                    <td>{p.cheese.name}</td>
                    <td>{p.sauce.name}</td>
                    <td>{p.toppings.map((t) => t.name).join(", ")}</td>
                    <td>${p.total.toFixed(2)}</td>
                    <td>
                    {loggedInUser?.roles?.includes("Admin") && (
                        <>
                        <Link to={`/orders/${order.id}/pizzas/${p.id}/edit`}>Edit</Link>
                        {" | "}
                        <Button
                            color="danger"
                            size="sm"
                            onClick={() => deletePizza(p.id).then(refreshOrder)}
                        >
                            Remove
                        </Button>
                        </>
                    )}
                    </td>
                </tr>
                ))}
            </tbody>
            </Table>
        ) : (
            <p className="text-muted mb-0">No pizzas have been added yet.</p>
        )}
        </CardBody>
      </Card>
        
        {loggedInUser?.roles?.includes("Admin") && (
        <Button
            color="danger"
            onClick={() => deleteOrder(order.id).then(() => navigate("/orders"))}
        >
            Cancel Order
        </Button>
        )}
    <Button color="secondary" onClick={() => navigate("/orders")}>
    Back to Orders
    </Button>
    </div>
  );
}