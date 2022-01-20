import React from "react"
import PostDetails from "./PostDetails"
import { useHistory } from "react-router"
import { deletePost, getAllPosts } from "../modules/postManager";


export const PostCard = ({ post, setPosts }) => {

    const history = useHistory();

    const handleClickDeletePost = () => {
        const confirm = window.confirm("Are you sure you would like to delete this post?")
        if (confirm === true) {
            deletePost(post)
                .then(() => {
                    getAllPosts().then(posts => setPosts(posts))
                }
                )
        } else {
            return;
        }
    }

    const handleClickEditPost = () => {
        history.push(`/Post/create/${post.id}`)
    }

    return (
        <>
            <h2>Title: {post.title}</h2>
            <h3>Category: {post.category.name}</h3>
            <h4>Author: {post.userprofile.displayName}</h4>
            <button onClick={() => history.push(`/Post/${post.id}`)}>Details</button>
            <button onClick={handleClickEditPost}>Edit</button>
            <button onClick={handleClickDeletePost}>Delete</button>
        </>
    )
}