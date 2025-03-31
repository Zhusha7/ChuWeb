import Navbar from './Navbar';
import Hero from './Hero';
import TwitchEmbed from './TwitchEmbed';
import ScrollAnimation from './ScrollAnimation';

const LandingPage = () => {
  return (
    <div className="min-h-screen bg-[#0f0f1a] text-white overflow-x-hidden">
      <Navbar />
      <Hero />
      
      <div className="container mx-auto px-4 py-16">
        {/* Twitch Stream Section */}
        <section className="mb-20">
          <ScrollAnimation animation="animate__fadeIn" initiallyVisible={true}>
            <h2 className="text-3xl font-bold mb-8 text-accent-pink">Latest Stream</h2>
          </ScrollAnimation>
          <TwitchEmbed channel="chucatherine" height="500px" />
        </section>
        
        {/* Latest Updates Section */}
        <section className="mb-20">
          <ScrollAnimation animation="animate__fadeIn" initiallyVisible={true}>
            <h2 className="text-3xl font-bold mb-8 text-accent-pink">Latest Updates</h2>
          </ScrollAnimation>
          
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {/* Blog Card */}
            <ScrollAnimation animation="animate__fadeInUp" initiallyVisible={true}>
              <div className="bg-[#191925] rounded-bevel p-6 shadow-xl hover-grow hover-glow">
                <h3 className="text-xl font-semibold mb-4 text-accent-blue">Blog</h3>
                <p className="text-gray-400 mb-4">
                  Check out my latest blog posts about gaming, streaming, and more!
                </p>
                <a 
                  href="#blog" 
                  className="inline-block bg-accent-blue text-white px-4 py-2 rounded-bevel hover:bg-blue-500 transition-colors"
                >
                  Read Blog
                </a>
              </div>
            </ScrollAnimation>
            
            {/* Music Card */}
            <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-1s" initiallyVisible={true}>
              <div className="bg-[#191925] rounded-bevel p-6 shadow-xl hover-grow hover-glow">
                <h3 className="text-xl font-semibold mb-4 text-accent-yellow">Music</h3>
                <p className="text-gray-400 mb-4">
                  Listen to my stream playlists and favorite tracks.
                </p>
                <a 
                  href="#music" 
                  className="inline-block bg-accent-yellow text-black px-4 py-2 rounded-bevel hover:bg-yellow-400 transition-colors"
                >
                  Listen Now
                </a>
              </div>
            </ScrollAnimation>
            
            {/* Social Media Card */}
            <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-2s" initiallyVisible={true}>
              <div className="bg-[#191925] rounded-bevel p-6 shadow-xl hover-grow hover-glow">
                <h3 className="text-xl font-semibold mb-4 text-accent-pink">Social</h3>
                <p className="text-gray-400 mb-4">
                  Follow me on social media to stay updated on my latest content.
                </p>
                <a 
                  href="#social" 
                  className="inline-block bg-accent-pink text-white px-4 py-2 rounded-bevel hover:bg-pink-600 transition-colors"
                >
                  Connect
                </a>
              </div>
            </ScrollAnimation>
          </div>
        </section>
      </div>
      
      {/* Footer */}
      <footer className="bg-[#161625] py-12 px-4">
        <div className="container mx-auto">
          <ScrollAnimation animation="animate__bounceIn" initiallyVisible={true}>
            <h2 className="text-2xl font-bold mb-6 text-accent-pink">ChuCatherine</h2>
          </ScrollAnimation>
          
          <ScrollAnimation animation="animate__fadeIn" initiallyVisible={true}>
            <div className="flex space-x-6 mb-8">
              <a 
                href="https://twitter.com/chucatherine" 
                target="_blank" 
                rel="noopener noreferrer"
                className="text-gray-400 hover:text-accent-pink transition-colors"
              >
                Twitter
              </a>
              <a 
                href="https://www.twitch.tv/chucatherine" 
                target="_blank" 
                rel="noopener noreferrer"
                className="text-gray-400 hover:text-accent-pink transition-colors"
              >
                Twitch
              </a>
              <a 
                href="https://www.instagram.com/chucatherine" 
                target="_blank" 
                rel="noopener noreferrer"
                className="text-gray-400 hover:text-accent-pink transition-colors"
              >
                Instagram
              </a>
              <a 
                href="https://t.me/chucatherine" 
                target="_blank" 
                rel="noopener noreferrer"
                className="text-gray-400 hover:text-accent-pink transition-colors"
              >
                Telegram
              </a>
            </div>
          </ScrollAnimation>
          
          <ScrollAnimation animation="animate__fadeIn" delay="animate__delay-1s" initiallyVisible={true}>
            <div className="text-gray-500 text-sm">
              &copy; {new Date().getFullYear()} ChuCatherine. All rights reserved.
            </div>
          </ScrollAnimation>
        </div>
      </footer>
    </div>
  );
};

export default LandingPage; 