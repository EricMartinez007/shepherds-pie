const _apiUrl = "/api/orders";

export const getOrders = (date) => {
  const url = date ? `${_apiUrl}?date=${date}` : _apiUrl;
  return fetch(url).then((res) => res.json());
};


export const getOrder = (id) => {
  return fetch(`${_apiUrl}/${id}`).then((res) => res.json());
};

export const createOrder = (order) => {
    return fetch(`${_apiUrl}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(order),
    }).then((res) => res.json());
}

export const deleteOrder = (id) => {
    return fetch(`${_apiUrl}/${id}`,{
        method: "DELETE"
    });
}

export const updateOrder = (order) => {
    return fetch(`${_apiUrl}/${order.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(order),
  });
}