import { Card, Container, Row, Col } from 'react-bootstrap';

ProfilePage.route = {
  path: '/profile',
  menuLabel: 'Profile',
  parent: '/'
};

export default function ProfilePage() {
  return (
    <Container className="mt-4">
      <Row>
        <Col lg={8} className="mx-auto">
          <Card>
            <Card.Body>
              <h1 className="card-title">My Profile</h1>
              <p className="card-text">
                Manage your profile information and preferences here.
              </p>
              {/* TODO: Implement profile management functionality */}
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
