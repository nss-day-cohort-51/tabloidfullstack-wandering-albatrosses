import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { getAllPosts } from "../modules/postManager";
import { PostCard } from "./PostCard";



export const Posts = ({ post }) => {
    const [posts, setPosts] = useState([]);

    const getPosts = () => {
        getAllPosts().then(res => setPosts(res))
    }
    useEffect(() => getPosts(), [])
    return (
        <>
            {posts.map(post => <PostCard key={post.id} allPosts={post} />)}
        </>
    )
}