import ScrollAnimation from './ScrollAnimation';
import ContactForm from './ContactForm';

const Hero = () => {
  return (
    <div className="min-h-screen pt-24 pb-12 px-4 md:px-8" id="home">
      <div className="container mx-auto">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
          {/* Hero Content */}
          <div className="order-2 lg:order-1">
            <ScrollAnimation animation="animate__fadeInLeft" initiallyVisible={true}>
              <h1 className="text-4xl md:text-5xl font-bold text-white mb-4">
                Welcome to <span className="text-accent-pink">ChuCatherine's</span> World
              </h1>
            </ScrollAnimation>
            
            <ScrollAnimation animation="animate__fadeInLeft" delay="animate__delay-1s" initiallyVisible={true}>
              <p className="text-xl text-gray-300 mb-6">
                Streamer, Content Creator, and Digital Artist
              </p>
            </ScrollAnimation>
            
            <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-2s" initiallyVisible={true}>
              <p className="text-gray-400 mb-8">
                Join me on my journey through gaming, art, and creative content.
                Let's build an amazing community together!
              </p>
            </ScrollAnimation>
            
            <ScrollAnimation animation="animate__fadeInUp" delay="animate__delay-3s" initiallyVisible={true}>
              <div className="flex flex-col sm:flex-row gap-4 mb-8">
                <a 
                  href="#stream" 
                  className="bg-accent-pink hover:bg-pink-600 text-white font-medium py-3 px-6 rounded-bevel transition-colors hover-grow"
                >
                  Watch Stream
                </a>
                <a 
                  href="#contact" 
                  className="bg-transparent border-2 border-accent-blue hover:bg-accent-blue hover:bg-opacity-20 text-white font-medium py-3 px-6 rounded-bevel transition-colors hover-grow"
                >
                  Get in Touch
                </a>
              </div>
            </ScrollAnimation>
            
            {/* Contact Form Section */}
            <div className="mt-12 bg-[#191925] p-6 rounded-bevel shadow-xl max-w-md">
              <ScrollAnimation animation="animate__fadeIn" initiallyVisible={true}>
                <h3 className="text-2xl font-semibold text-white mb-4">
                  Send me a message
                </h3>
              </ScrollAnimation>
              <ContactForm />
            </div>
          </div>
          
          {/* Hero Image */}
          <div className="order-1 lg:order-2 relative">
            <ScrollAnimation animation="animate__fadeInRight" initiallyVisible={true}>
              <div className="relative z-10">
                <img 
                  src="https://placehold.co/600x400/ff71ce/ffffff?text=ChuCatherine" 
                  alt="ChuCatherine" 
                  className="w-full max-w-lg mx-auto rounded-bevel shadow-2xl animate__animated animate__pulse animate__infinite animate__slow"
                />
              </div>
            </ScrollAnimation>
            
            {/* Decorative Elements */}
            <div className="absolute top-[-20px] right-[-20px] w-[150px] h-[150px] bg-accent-blue rounded-full opacity-30 blur-2xl animate__animated animate__slideInRight"></div>
            <div className="absolute bottom-[-30px] left-[-30px] w-[180px] h-[180px] bg-accent-yellow rounded-full opacity-20 blur-2xl animate__animated animate__slideInLeft"></div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Hero; 