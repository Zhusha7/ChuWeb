import { useEffect, useState, useRef } from 'react';
import ScrollAnimation from './ScrollAnimation';

// Add TypeScript declarations for Twitch Embed API
declare global {
  interface Window {
    Twitch?: {
      Embed: new (
        elementId: string, 
        options: {
          width: string | number;
          height: string | number;
          channel?: string;
          video?: string;
          collection?: string;
          parent: string[];
          autoplay?: boolean;
          muted?: boolean;
          layout?: string;
        }
      ) => TwitchEmbed;
    };
  }
}

// Type definition for Twitch Embed instance
interface TwitchEmbed {
  addEventListener: (event: string, callback: () => void) => void;
  getPlayer: () => {
    isPaused: () => boolean;
    getEnded: () => boolean;
    pause: () => void;
    play: () => void;
  };
}

interface TwitchEmbedProps {
  channel: string;
  width?: string;
  height?: string;
}

const TwitchEmbedComponent = ({ channel, height = '400px' }: TwitchEmbedProps) => {
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);
  
  const embedRef = useRef<HTMLDivElement>(null);
  
  useEffect(() => {
    let embed: TwitchEmbed | null = null;
    const fallbackTimeout = setTimeout(() => {
      setIsLoading(false);
      setHasError(true);
    }, 10000); // 10 seconds max loading time
    
    const loadScript = () => {
      return new Promise<void>((resolve, reject) => {
        if (window.Twitch) {
          resolve();
          return;
        }
        
        const script = document.createElement('script');
        script.src = 'https://embed.twitch.tv/embed/v1.js';
        script.async = true;
        
        script.onload = () => {
          resolve();
        };
        script.onerror = () => {
          reject(new Error("Failed to load Twitch embed script"));
        };
        
        document.body.appendChild(script);
      });
    };

    const init = async () => {
      try {
        await loadScript();
        
        if (!window.Twitch) {
          throw new Error('Twitch embed script failed to load properly');
        }
        
        if (embedRef.current && window.Twitch) {
          // Get the current hostname or use localhost for development
          const parent = window.location.hostname === '' ? ['localhost'] : [window.location.hostname];
          
          embed = new window.Twitch.Embed(embedRef.current.id, {
            width: '100%',
            height,
            channel,
            layout: 'video',
            autoplay: false,
            parent
          });
          
          embed.addEventListener('READY', () => {
            setIsLoading(false);
            clearTimeout(fallbackTimeout);
          });
        }
      } catch (error) {
        console.error('Error initializing Twitch embed:', error);
        setHasError(true);
        setIsLoading(false);
        clearTimeout(fallbackTimeout);
      }
    };
    
    init();
    
    return () => {
      clearTimeout(fallbackTimeout);
    };
  }, [channel, height]);
  
  return (
    <div className="rounded-bevel overflow-hidden shadow-lg bg-accent-blue bg-opacity-10 p-2" id="stream">
      <ScrollAnimation animation="animate__backInUp" initiallyVisible={true}>
        <div className="text-xl font-semibold text-accent-pink mb-2 px-2">
          Live Stream
        </div>
      </ScrollAnimation>
      
      <div className="rounded-bevel overflow-hidden">
        {isLoading && (
          <div className="flex items-center justify-center h-64 bg-accent-blue bg-opacity-10">
            <div className="text-accent-pink">Loading Twitch content...</div>
          </div>
        )}
        
        {hasError ? (
          <div className="flex flex-col items-center justify-center h-64 bg-accent-blue bg-opacity-10 p-4 text-center">
            <div className="text-accent-pink mb-2">Could not load Twitch content</div>
            <div className="text-sm text-gray-600 mb-4">Check out the channel directly on Twitch</div>
            <a 
              href={`https://www.twitch.tv/${channel}`} 
              target="_blank" 
              rel="noopener noreferrer"
              className="bg-accent-pink text-white px-4 py-2 rounded-bevel hover:bg-pink-600 transition-colors"
            >
              Visit Channel
            </a>
          </div>
        ) : (
          <ScrollAnimation animation="animate__zoomIn" initiallyVisible={true}>
            <div 
              id="twitch-embed" 
              ref={embedRef}
              className="w-full"
              style={{ height, display: isLoading ? 'none' : 'block' }}
            ></div>
          </ScrollAnimation>
        )}
      </div>
      
      <ScrollAnimation animation="animate__fadeIn" delay="animate__delay-1s" initiallyVisible={true}>
        <div className="text-sm text-gray-600 mt-2 px-2">
          Follow me on <a href={`https://www.twitch.tv/${channel}`} target="_blank" rel="noopener noreferrer" className="text-accent-pink hover:underline">Twitch</a> for live streams!
        </div>
      </ScrollAnimation>
    </div>
  );
};

export default TwitchEmbedComponent; 