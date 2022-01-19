import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import { Posts } from "./Post";
import { TagList } from "./TagList.js";
import TagForm from "./TagForm";
import UserList from "./UserList";
import PostDetails from "./PostDetails";

export default function ApplicationViews({ isLoggedIn }) {

  return (
    <main>
      <Switch>
        <Route path="/" exact>
          Home
        </Route>

        <Route path="/Post" exact>
          {isLoggedIn ? <Posts /> : <Redirect to="/login" />}
        </Route>

        <Route path="/Post/:id">
          {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/users">
          {isLoggedIn ? <UserList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/login">
          <Login />
        </Route>

        <Route path="/register">
          <Register />
        </Route>

        <Route path="/tag" exact>
          {isLoggedIn ? <TagList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/tag/create" exact>
          {isLoggedIn ? <TagForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/tag/create/:id" exact>
          {isLoggedIn ? <TagForm /> : <Redirect to="/login" />}
        </Route>
      </Switch>
    </main>
  );
};