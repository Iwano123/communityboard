import { Card, Container, Row, Col } from 'react-bootstrap';

AdminPanelPage.route = {
  path: '/admin',
  menuLabel: 'Admin',
  parent: '/'
};

export default function AdminPanelPage() {
  return (
    <Container className="mt-4">
      <Row>
        <Col lg={8} className="mx-auto">
          <Card>
            <Card.Body>
              <h1 className="card-title">Admin Panel</h1>
              <p className="card-text">
                Manage posts, users, and system settings.
              </p>
              {/* TODO: Implement admin functionality */}
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
