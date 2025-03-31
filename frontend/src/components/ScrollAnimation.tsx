import { ReactNode, useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';

interface ScrollAnimationProps {
  children: ReactNode;
  animation: string;
  delay?: string;
  duration?: string;
  threshold?: number;
  triggerOnce?: boolean;
  className?: string;
  initiallyVisible?: boolean;
}

const ScrollAnimation = ({
  children,
  animation,
  delay = '',
  duration = '',
  threshold = 0.1,
  triggerOnce = true,
  className = '',
  initiallyVisible = false,
}: ScrollAnimationProps) => {
  const [hasAnimated, setHasAnimated] = useState(false);
  const { ref, inView } = useInView({
    threshold,
    triggerOnce,
  });

  useEffect(() => {
    if (inView && !hasAnimated) {
      setHasAnimated(true);
    }
  }, [inView, hasAnimated]);

  // Don't hide content initially, just don't animate it
  const animationClasses = hasAnimated
    ? `animate__animated ${animation} ${delay} ${duration}`
    : initiallyVisible ? '' : 'opacity-50 transition-opacity duration-300';

  return (
    <div ref={ref} className={`${animationClasses} ${className}`}>
      {children}
    </div>
  );
};

export default ScrollAnimation; 