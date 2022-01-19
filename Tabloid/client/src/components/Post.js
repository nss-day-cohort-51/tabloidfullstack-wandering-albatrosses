import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { getAllPosts } from "../modules/postManager";
import { PostCard } from "./PostCard";

//This is the Post List

export const Posts = ({ post }) => {
    const [posts, setPosts] = useState([]);

    const getPosts = () => {
        getAllPosts().then(res => setPosts(res))
    }
    useEffect(() => getPosts(), [])
    return (
        <>
            {posts.map(post => <PostCard key={post.id} post={post} />)}
        </>
    )
}