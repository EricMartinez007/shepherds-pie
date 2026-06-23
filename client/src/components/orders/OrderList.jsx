import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Table } from "reactstrap";
import { getOrders } from "../../managers/orderManager";

export default function OrderList({ loggedInUser }) {
  const [orders, setOrders] = useState([]);
  const [filterDate, setFilterDate] = useState(new Date().toISOString().split("T")[0]);

  useEffect(() => {
    getOrders(filterDate).then(setOrders);
  }, [filterDate]);

  return (
    <div className="container mt-4">
      <h2>Orders</h2>
        {loggedInUser?.roles?.includes("Admin") && (
            <Link to="/orders/create">
                <Button color="primary" className="mb-3">Create New Order</Button>
            </Link>
        )}
        <input type="date" value={filterDate} onChange={(e) => setFilterDate(e.target.value)} />
      <Table striped className="mt-3">
        <thead>
          <tr>
            <th>Order Id</th>
            <th>Order Date</th>
            <th>Employee</th>
            <th></th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {orders.map((o) => (
            <tr key={o.id}>
              <td>
                {o.id} 
              </td>
              <td>{new Date(o.orderDate).toLocaleString()}</td>
              <td>{o.employee.firstName} {o.employee.lastName}</td>
              <td></td>
              <td>
                {loggedInUser?.roles?.includes("Admin") && (
                    <Link to={`/orders/${o.id}`}>Details</Link>
                )}
              </td>
              <td></td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
}