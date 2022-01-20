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