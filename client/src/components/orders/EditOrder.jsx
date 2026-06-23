import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { getOrder, updateOrder } from "../../managers/orderManager";
import { getUserProfiles } from "../../managers/userProfileManager";

export default function EditOrder() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [order, setOrder] = useState(null);
    const [employees, setEmployees] = useState([]);
    const [tableNumber, setTableNumber] = useState("");
    const [tip, setTip] = useState("");
    const [delivererId, setDelivererId] = useState(0);
    const [isDelivery, setIsDelivery] = useState(false);

    useEffect(() => {
        getOrder(id).then((o) => {
        setOrder(o);
        setTableNumber(o.tableNumber ?? "");
        setTip(o.tip ?? "");
        setDelivererId(o.delivererId ?? 0);
        setIsDelivery(o.isDelivery);
        });
        getUserProfiles().then(setEmployees);
    }, [id]);

    const handleSubmit = (e) => {
        e.preventDefault();
        const updatedOrder = {
            tableNumber: isDelivery ? null : parseInt(tableNumber),
            tip: tip ? parseFloat(tip) : null,
            delivererId: isDelivery ? parseInt(delivererId) : null,
        };
        updateOrder({ ...updatedOrder, id: parseInt(id) })
        .then(() => navigate(`/orders/${id}`));
    };
    
    if (!order) return <p>Loading...</p>;
    return (
        <>
        <h2>Edit Order</h2>
        <Form>
            <FormGroup>
            <Label>Delivery</Label>
                <Input
                type="checkbox"
                value={isDelivery}
                checked={isDelivery}
                onChange={(e) => setIsDelivery(e.target.checked)}
                />
                Yes
            </FormGroup>
            {isDelivery && (
            <FormGroup>
            <Label>Deliverer</Label>
                <Input 
                    type="select" 
                    value={delivererId} 
                    onChange={(e) => setDelivererId(e.target.value)}
                >
                <option value={0}>-- Select a deliverer --</option>
                {employees.map((e) => (
                    <option key={e.id} value={e.id}>{e.firstName} {e.lastName}</option>
                ))}
                </Input>
            </FormGroup>
            )}
            {!isDelivery && (
            <FormGroup>
            <Label>Table Number</Label>
                <Input
                type="text"
                value={tableNumber}
                onChange={(e) => setTableNumber(e.target.value)}
                />
            </FormGroup>
            )}
            <FormGroup>
            <Label>Tip</Label>
            <Input
                type="number"
                value={tip}
                onChange={(e) => setTip(e.target.value)}
            />
            </FormGroup>
            <Button onClick={handleSubmit} color="primary">
            Submit
            </Button>
        </Form>
        </>
    );
}
