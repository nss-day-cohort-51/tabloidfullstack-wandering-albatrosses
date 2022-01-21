import React from "react";
import { useHistory, useParams } from "react-router";
import { Container } from "reactstrap";
import { addPost, getPost, updatePost } from "../modules/postManager";
import { useState } from "react";

const PostForm = () => {

    const [post, setPost] = useState({
        title: "",
    })

    const postId = useParams();

    const history = useHistory();

    if (postId.id && post.title === "") {
        getPost(postId.id)
            .then(post => setPost(post));
    }

    const handleInput = (event) => {
        const newPost = { ...post };
        newPost[event.target.id] = event.target.value;
        setPost(newPost);
    }

    const handleCreatePost = () => {
        addPost(post)
            .then(history.push("/Post"))
    }

    const handleClickUpdatePost = () => {
        updatePost(post)
            .then(history.push("/Post"))
    }

    const handleClickCancel = () => {
        history.push("/Post")
    }

    return (
        <div className="postForm">
            <h2>Post</h2>
            <div className="container-5">
                <div className="form-group">
                    <form>
                        <label htmlFor="title">Title</label>
                        <input type="text" className="form-control" id="title" placeholder="Title" onChange={handleInput} required />

                        <label htmlFor="content">Content</label>
                        <input type="text" className="form-control" id="content" placeholder="Content" onChange={handleInput} required />

                        <label htmlFor="categoryId">Category</label>
                        <input type="text" className="form-control" id="categoryId" placeholder="Category" onChange={handleInput} required />

                        <label htmlFor="imageUrl">Image Url</label>
                        <input type="text" className="form-control" id="imageUrl" placeholder="ImageUrl" onChange={handleInput} />

                        <label htmlFor="publishDateTime">Publication Date</label>
                        <input type="text" className="form-control" id="publishDateTime" placeholder="Publication Date" onChange={handleInput} />
                    </form>
                </div>
                {postId.id ?
                    <div>
                        <button type="submit" onClick={event => {
                            handleClickUpdatePost()
                        }}>Update</button>

                        <button type="cancel" onClick={event => {
                            handleClickCancel()
                        }}>Cancel</button>

                    </div>
                    :
                    <button type="submit" onClick={event => {
                        handleCreatePost()
                    }}>Create</button>}
            </div>
        </div>
    )
}

export default PostForm;