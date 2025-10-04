export default async function postsLoader() {
  try {
    const response = await fetch('http://localhost:5002/api/posts');
    if (!response.ok) {
      throw new Error('Failed to fetch posts');
    }
    const posts = await response.json();
    return { posts };
  } catch (error) {
    console.error('Error loading posts:', error);
    return { posts: [] };
  }
}