import { getToken } from "./authManager";
const baseUrl = '/api/UserProfile'

export const getAllUsers = () => {

    return getToken().then(token => {
        return fetch(`${baseUrl}/GetAllUsers`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(resp => resp.json())
    })
    
}

export const getUser = (id) => {
    return getToken().then((token) => {
    return fetch(`${baseUrl}/GetUserProfileById/${id}`, {
        method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
    }
    
)
    
    .then((res) => res.json());
})}

export const getAllUserTypes = () => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/GetUserTypes`, {
            method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`
                }
        }
        
    )
        
        .then((res) => res.json());
    })}



export const updateUserType2 = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/UpdateUserType2/userId/${id}`, {
            method: "PUT",
                headers: {
                    Authorization: `Bearer ${token}`
                }
        }
        
    )
        
        .then((res) => res.json());
})}

export const updateUserType1 = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/UpdateUserType1/userId/${id}`, {
            method: "PUT",
                headers: {
                    Authorization: `Bearer ${token}`
                }
        }
        
    )
        
        .then((res) => res.json());
})}

