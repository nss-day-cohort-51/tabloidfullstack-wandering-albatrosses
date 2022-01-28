import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import User from "./User";
import { getUser } from "../modules/userManager";

const UserDetails = () => {
    const [user, setUser] = useState({});
    const { id } = useParams();

    useEffect(() => {
        getUser(id).then(res => setUser(res));
    }, []);

    if (!user) {
        return null;
    }

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    <p>Name: {user.firstName}, {user.lastName}</p>
                    <p>Email: {user.email}</p>
                    <p>Display name: {user.displayName}</p>
                    <p>Creation Date: {user.createDateTime}</p>
                    <p>User Type: {user.userTypeId}</p>
                </div>
            </div>
        </div>
    );
};

export default UserDetails;