import React from "react"


export const PostCard = ({ allPosts }) => {
    return (
        <>
            <h2>{allPosts.title}</h2>
            <h2>Category: {allPosts.category.Name}</h2>
        </>
    )
}