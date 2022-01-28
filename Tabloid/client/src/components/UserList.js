import React, { useEffect, useState } from "react";
import User from './User';
import { getAllUsers } from "../modules/userManager";

const UserList = () => {
    const [users, setUsers] = useState([]);

    const getUsers = () => {
        getAllUsers().then(users => 
            {setUsers(users)});
    };

    useEffect(() => {
        getUsers();
    }, []);


    return (
        <div className="row justify-content-center">
            {users.map((user) => (
                <User user={user} key={user.id} />
            ))}
        </div>
    );
};

export default UserList;