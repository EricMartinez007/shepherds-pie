import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { createPizza } from "../../managers/pizzaManager";
import { getSizes } from "../../managers/sizeManager";
import { getCheeses } from "../../managers/cheeseManager";
import { getSauces } from "../../managers/sauceManager";
import { getToppings } from "../../managers/toppingManager";

export default function CreatePizza() {
  const { orderId } = useParams();
  const navigate = useNavigate();

  const [sizes, setSizes] = useState([]);
  const [cheeses, setCheeses] = useState([]);
  const [sauces, setSauces] = useState([]);
  const [toppings, setToppings] = useState([]);

  const [sizeId, setSizeId] = useState(0);
  const [cheeseId, setCheeseId] = useState(0);
  const [sauceId, setSauceId] = useState(0);
  const [selectedToppingIds, setSelectedToppingIds] = useState([]);

  useEffect(() => {
    getSizes().then(setSizes);
    getCheeses().then(setCheeses);
    getSauces().then(setSauces);
    getToppings().then(setToppings);
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    const newPizza = {
      orderId: parseInt(orderId),
      sizeId: parseInt(sizeId),
      cheeseId: parseInt(cheeseId),
      sauceId: parseInt(sauceId),
      toppingIds: selectedToppingIds,
    };
    createPizza(newPizza).then(() => navigate(`/orders/${orderId}`));
  };

  return (
    <>
      <h2>Create A Pizza</h2>
      <Form>
        <FormGroup>
          <Label>Size</Label>
            <Input 
                type="select" 
                value={sizeId} 
                onChange={(e) => setSizeId(e.target.value)}
            >
            <option value={0}>-- Select a size --</option>
            {sizes.map((s) => (
                <option key={s.id} value={s.id}>{s.name} (${s.price})</option>
            ))}
            </Input>
        </FormGroup>
        <FormGroup>
          <Label>Cheese</Label>
            <Input 
                type="select" 
                value={cheeseId} 
                onChange={(e) => setCheeseId(e.target.value)}
            >
            <option value={0}>-- Select a cheese --</option>
            {cheeses.map((c) => (
                <option key={c.id} value={c.id}>{c.name}</option>
            ))}
            </Input>
        </FormGroup>
        <FormGroup>
          <Label>Sauce</Label>
            <Input 
                type="select" 
                value={sauceId} 
                onChange={(e) => setSauceId(e.target.value)}
            >
            <option value={0}>-- Select a sauce --</option>
            {sauces.map((s) => (
                <option key={s.id} value={s.id}>{s.name}</option>
            ))}
            </Input>
        </FormGroup>
        <FormGroup>
          <Label>Toppings</Label>
          {toppings.map((t) => (
            <div key={t.id}>
                <Input
                type="checkbox"
                checked={selectedToppingIds.includes(t.id)}
                onChange={() => {
                    setSelectedToppingIds((prev) =>
                    prev.includes(t.id)
                        ? prev.filter((id) => id !== t.id)
                        : [...prev, t.id]
                    );
                }}
                />
                {" "}{t.name} (${t.price})
            </div>
            ))}
        </FormGroup>
        <Button onClick={handleSubmit} color="primary">
          Submit
        </Button>
      </Form>
    </>
  );
}