import React from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory } from "react-router-dom/cjs/react-router-dom.min";

const User = ({ user }) => {
    const history = useHistory()
    {
        return (
            <Card >
                <p className="text-left px-2"> Display Name: {user.displayName}</p>
                <CardBody>
                   
                <p>Name: {user.firstName} {user.lastName}</p>
                <p>User Type: {user.userTypeId}</p>    
                <button onClick={() => history.push(`/users/userdetails/${user.id}`)}> User Details</button>
                <button onClick={() => history.push(`/users/UserTypeEdit/${user.id}`)}> edit user</button>
                </CardBody>
            </Card>
        );
    }


};

export default User;