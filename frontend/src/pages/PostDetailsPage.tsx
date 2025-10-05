import { Card, Container, Row, Col } from 'react-bootstrap';

PostDetailsPage.route = {
  path: '/post/:id',
  parent: '/'
};

export default function PostDetailsPage() {
  return (
    <Container className="mt-4">
      <Row>
        <Col lg={8} className="mx-auto">
          <Card>
            <Card.Body>
              <h1 className="card-title">Post Details</h1>
              <p className="card-text">
                View detailed information about this post.
              </p>
              {/* TODO: Implement post details functionality */}
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
