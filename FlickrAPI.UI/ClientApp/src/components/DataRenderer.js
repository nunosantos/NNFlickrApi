import React from 'react';
import { Card, CardHeader, Container, Row, Col } from 'reactstrap';

const DataRenderer = (props) => {
    return (
        <Container>
            <Row>
                {props.data.map((photo) =>
                    <Col xs={6} sm={{ span: 6 }} md={4}
                        lg={{ span: 6 }} xl={{ span: 1 }}>
                        <Card className="card-margin">
                            <CardHeader>{photo.title}</CardHeader>
                            <img width="100%" src={`http://farm${photo.farm}.staticflickr.com/${photo.server}/${photo.id}_${photo.secret}.jpg`} alt="Card image cap" />
                        </Card>
                    </Col>
                )};
                </Row>
        </Container >
    );
}

export default DataRenderer;