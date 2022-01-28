import React, { useState } from "react";
import { useHistory } from "react-router";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { getAllUserTypes } from "../modules/userManager";

export const UpdateUserForm = () => {

    const { id } = useParams();

    const [user, setUser] = useState([]);



    const history = useHistory();

    const getUserType = () => {
        getAllUserTypes(id).then(resp => {
            setUser(resp);
        })
    };

    useEffect(() => {
        getUserType();
    }, []);

    return (
    <>
        {
            <select>
            { 
               user.map(userId => 
                <>
                    
                       <option key={userId.key} value={userId.id}>
                           {userId.name}
                       </option>
                        
                      
                        </>
                     
            )}   
            </select>
        }
    </>
    )


};

