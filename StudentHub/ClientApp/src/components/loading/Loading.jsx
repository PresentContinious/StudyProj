import styles from './loadin.module.css';
import {useEffect, useState} from "react";

const LoadingPage = ({loading}) => {
    const [progress, setProgress] = useState(0);

    useEffect(() => {
        if (loading) {
            const timer = setInterval(() => {
                if (progress >= 100) {
                    setProgress(0);
                } else
                    setProgress(progress + .5);
            }, 20);
            return () => clearInterval(timer);
        }
    });

    useEffect(() => {
        if (loading) {
            document.body.style.overflow = 'hidden';
            document.body.style.width = '100%';
            document.body.style.height = '100%';
        } else {
            document.body.style.overflow = 'auto';
            document.body.style.width = 'auto';
            document.body.style.height = 'auto';
        }
    }, [loading]);

    if (!loading) return <></>;

    return (
        <div className={styles.loadingPage}>
            <div className={styles.loadingPageV}>
                <div className={styles.overlap}>
                    <div className={styles.group}>
                        <img alt="Logo" src="https://c.animaapp.com/mNPT6aia/img/----------------.svg"/>
                        <div className={styles.divWrapper}>
                            <p className={styles.div}>
                                Опануйте актуальні навички на онлайн-курсах від кращих викладачів на найбільшій
                                освітній платформі України!
                            </p>
                        </div>
                        <div className={styles.rectangle}>
                            <div className={styles.rect} style={{width: `${progress}%`}}></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoadingPage;