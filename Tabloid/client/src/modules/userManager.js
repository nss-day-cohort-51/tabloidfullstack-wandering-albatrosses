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