import React from "react";
import { Card, CardBody } from "reactstrap";

const User = ({ user }) => {

    {
        return (
            <Card >
                <p className="text-left px-2"> Display Name: {user.displayName}</p>
                <CardBody>
                   
                <p>Name: {user.firstName} {user.lastName}</p>
                <p>User Type: {user.userTypeId}</p>    

                </CardBody>
            </Card>
        );
    }


};

export default User;