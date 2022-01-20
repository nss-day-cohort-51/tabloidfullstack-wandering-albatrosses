import React from "react"
import PostDetails from "./PostDetails"
import { useHistory } from "react-router"


export const PostCard = ({ post }) => {
    const history = useHistory();
    return (
        <>
            <h2>Title: {post.title}</h2>
            <h3>Category: {post.category.name}</h3>
            <h4>Author: {post.userprofile.displayName}</h4>
            <button onClick={() => history.push(`/Post/${post.id}`)}>Details</button>
        </>
    )
}