import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { Posts } from "./Post";

export default function PostViews({ isLoggedIn }) {

    return (
        <main>
            <Switch>
                <Route path="/Post" exact>
                    <Posts />
                </Route>


            </Switch>
        </main>
    );
};
