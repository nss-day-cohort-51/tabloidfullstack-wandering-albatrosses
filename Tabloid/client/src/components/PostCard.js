import React from "react"


export const PostCard = ({ allPosts }) => {
    return (
        <>
            <h2>Title: {allPosts.title}</h2>
            <h3>Category: {allPosts.category.name}</h3>
            <h4>Author: {allPosts.userprofile.displayName}</h4>
        </>
    )
}