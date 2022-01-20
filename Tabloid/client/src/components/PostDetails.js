import React, { useEffect, useState } from "react";
import { Posts } from "./Post";
import { getPost } from "../modules/postManager";
import { useParams } from "react-router";

const PostDetails = () => {
    const [post, setPost] = useState({});
    const { id } = useParams();

    useEffect(() => {
        getPost(id).then(res => setPost(res));
    }, []);

    if (!post) {
        return <h1>GET THE POST</h1>;
    }

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    <h1>Title: {post.title}</h1>
                    <h2>Content: {post.content}</h2>
                    <h3>Publish Date: {post.publishedDateTime}</h3>
                    {/* <h4>Author: {post.userprofile.displayName}</h4> */}
                </div>
            </div>
        </div>
    );
};

export default PostDetails;