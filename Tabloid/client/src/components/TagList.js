import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { getTags } from "../modules/tagManager";
import Tag from "./Tag";
import { Row, Col } from "reactstrap"

export const TagList = () => {

    const [tags, setTags] = useState([])

    const history = useHistory();

    const getAllTags = () => {
        getTags().then(tags => setTags(tags));
    };

    useEffect(() => {
        getAllTags()
    }, []);

    const handleClickTagForm = () => {
        history.push("/tag/create")
    }

    return (
        <div className="container">
            <div className="justify-content-center">
                <center><h1>Tags</h1></center>
                <Row xs="3">
                    <Col>
                    </Col>
                    <Col className="mt-3">
                    </Col>
                </Row>
                <p>
                    {tags.map((tag) => (
                        <Tag tag={tag} key={tag.Id} setTags={setTags} />
                    ))}
                    <button onClick={handleClickTagForm}>Create a Tag </button>
                </p>
            </div>
        </div>
    );
}