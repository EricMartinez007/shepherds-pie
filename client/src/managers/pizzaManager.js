const _apiUrl = "/api/pizzas";

export const createPizza = (pizza) => {
    return fetch(`${_apiUrl}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(pizza),
    }).then((res) => res.json());
}

export const deletePizza = (id) => {
    return fetch(`${_apiUrl}/${id}`,{
        method: "DELETE"
    });
}

export const updatePizza = (pizza) => {
    return fetch(`${_apiUrl}/${pizza.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(pizza),
  });
}

export const getPizza = (id) => {
    return fetch (`${_apiUrl}/${id}`).then((res) => res.json());
}