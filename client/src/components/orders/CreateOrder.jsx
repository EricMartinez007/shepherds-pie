import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { createOrder } from "../../managers/orderManager";

export default function CreateOrder({ loggedInUser }) {
  const navigate = useNavigate();
  const [tableNumber, setTableNumber] = useState("");
  const [isDelivery, setIsDelivery] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    const newOrder = {
      employeeId: loggedInUser.id,
      tableNumber: isDelivery ? null : parseInt(tableNumber),
    };
    createOrder(newOrder).then((res) => navigate(`/orders/${res.id}`));
  };

  return (
    <>
      <h2>Create Order</h2>
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
        <Button onClick={handleSubmit} color="primary">
          Submit
        </Button>
      </Form>
    </>
  );
}
