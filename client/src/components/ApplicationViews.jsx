import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import OrderList from "./orders/OrderList";
import CreateOrder from "./orders/CreateOrder";
import OrderDetail from "./orders/OrderDetails";
import CreatePizza from "./pizzas/CreatePizza";
import EditPizza from "./pizzas/EditPizza";
import EditOrder from "./orders/EditOrder";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <OrderList loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="orders">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <OrderList loggedInUser={loggedInUser}/>
            </AuthorizedRoute>
          }
        />
        <Route
          path="create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreateOrder loggedInUser={loggedInUser}/>
            </AuthorizedRoute>
          }
        />
        <Route
          path=":id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <OrderDetail loggedInUser={loggedInUser}/>
            </AuthorizedRoute>
          }
        />
        <Route
          path=":id/edit"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <EditOrder loggedInUser={loggedInUser}/>
            </AuthorizedRoute>
          }
        />
        <Route
          path=":orderId/pizzas/create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreatePizza />
            </AuthorizedRoute>
          }
        />
        <Route
          path=":orderId/pizzas/:id/edit"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <EditPizza />
            </AuthorizedRoute>
          }
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}