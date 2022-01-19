import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteTag, getTags } from "../modules/tagManager"
import { useHistory } from "react-router-dom";

const Tag = ({ tag, setTags }) => {

    const history = useHistory();

    const handleClickDeleteTag = () => {
        const confirm = window.confirm("Are you sure you want to delete this tag?")
        if (confirm === true) {
            deleteTag(tag)
                .then(() => {
                    getTags().then(tags => setTags(tags))
                }
                )
        } else {
            return;
        }

    }

    const handleClickEditTag = () => {
        history.push(`/tag/create/${tag.id}`)
    }

    return (
        <Card>
            <CardBody>
                <Row>
                    <Col>
                        <strong>{tag.name}</strong>
                    </Col>
                    <Col>
                        <Button onClick={handleClickEditTag}>Edit</Button>
                    </Col>
                    <Col>
                        <Button onClick={handleClickDeleteTag}>Delete</Button>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    )
}

export default Tag