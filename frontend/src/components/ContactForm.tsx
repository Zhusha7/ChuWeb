import { useState } from 'react';
import ScrollAnimation from './ScrollAnimation';

const ContactForm = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    message: ''
  });
  
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState('');
  
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    try {
      setIsSubmitting(true);
      setError('');
      
      // Simulate API call - replace with actual API call
      await new Promise(resolve => setTimeout(resolve, 1000));
      // In a real app, you would call your API here
      // Example: await apiService.contact(formData);
      
      setSuccess(true);
      setFormData({
        name: '',
        email: '',
        message: ''
      });
    } catch (err) {
      setError('Failed to send message. Please try again later.');
      console.error('Form submission error:', err);
    } finally {
      setIsSubmitting(false);
    }
  };
  
  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      {success ? (
        <ScrollAnimation animation="animate__fadeIn" initiallyVisible={true}>
          <div className="bg-green-500 bg-opacity-20 border border-green-500 text-white p-4 rounded-bevel">
            <p>Thank you for your message! I'll get back to you soon.</p>
          </div>
        </ScrollAnimation>
      ) : (
        <>
          {error && (
            <ScrollAnimation animation="animate__shakeX" initiallyVisible={true}>
              <div className="bg-red-500 bg-opacity-20 border border-red-500 text-white p-4 rounded-bevel">
                <p>{error}</p>
              </div>
            </ScrollAnimation>
          )}
          
          <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-1s" initiallyVisible={true}>
            <div>
              <label htmlFor="name" className="block text-sm font-medium text-gray-300 mb-1">
                Name
              </label>
              <input
                type="text"
                id="name"
                name="name"
                value={formData.name}
                onChange={handleChange}
                required
                className="w-full bg-[#121218] text-white border border-gray-700 rounded-bevel px-4 py-2 focus:outline-none focus:ring-2 focus:ring-accent-pink transition-all hover-grow"
                placeholder="Your name"
              />
            </div>
          </ScrollAnimation>
          
          <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-2s" initiallyVisible={true}>
            <div>
              <label htmlFor="email" className="block text-sm font-medium text-gray-300 mb-1">
                Email
              </label>
              <input
                type="email"
                id="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
                className="w-full bg-[#121218] text-white border border-gray-700 rounded-bevel px-4 py-2 focus:outline-none focus:ring-2 focus:ring-accent-pink transition-all hover-grow"
                placeholder="your.email@example.com"
              />
            </div>
          </ScrollAnimation>
          
          <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-3s" initiallyVisible={true}>
            <div>
              <label htmlFor="message" className="block text-sm font-medium text-gray-300 mb-1">
                Message
              </label>
              <textarea
                id="message"
                name="message"
                value={formData.message}
                onChange={handleChange}
                required
                rows={4}
                className="w-full bg-[#121218] text-white border border-gray-700 rounded-bevel px-4 py-2 focus:outline-none focus:ring-2 focus:ring-accent-pink transition-all hover-grow"
                placeholder="Your message..."
              />
            </div>
          </ScrollAnimation>
          
          <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-4s" initiallyVisible={true}>
            <button
              type="submit"
              disabled={isSubmitting}
              className={`w-full bg-accent-pink hover:bg-pink-600 text-white font-medium py-2 px-4 rounded-bevel transition-colors ${
                isSubmitting ? 'opacity-70 cursor-not-allowed' : 'hover-grow'
              }`}
            >
              {isSubmitting ? 'Sending...' : 'Send Message'}
            </button>
          </ScrollAnimation>
        </>
      )}
    </form>
  );
};

export default ContactForm; 