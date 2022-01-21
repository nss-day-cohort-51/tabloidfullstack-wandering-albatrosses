import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { getAllPosts } from "../modules/postManager";
import { PostCard } from "./PostCard";
import { useHistory } from "react-router";

//This is the Post List

export const Posts = ({ post }) => {

    const [posts, setPosts] = useState([]);

    const history = useHistory();

    const getPosts = () => {
        getAllPosts().then(res => setPosts(res))
    }

    useEffect(() => {
        getPosts()
    }, []);

    const handleClickPostForm = () => {
        history.push("/Post/create")
    }

    return (
        <>
            {posts.map(post => <PostCard key={post.id} post={post} />)}
            <button onClick={() => history.push("/Post/create")}>Create a Post</button>
        </>
    )
}