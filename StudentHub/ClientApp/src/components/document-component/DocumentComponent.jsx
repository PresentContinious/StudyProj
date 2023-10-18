import styles from './document.module.css';
import {useState} from "react";
import {NextButton} from "../video-component/VideoComponent";

const DocumentComponent = ({hidden, documents, fullScreen, courseId, show}) => {
    const [hide, setHide] = useState(false);
    const [current, setCurrent] = useState(0);
    const max = documents.length - 1;

    return (
        <div className={styles.group}>
            {show && <NextButton fullScreen={fullScreen} courseId={courseId}/>}
            <div className={styles.overlapWrapper}>
                <div className={`${styles.overlap}`} onMouseEnter={() => setHide(false)}
                     onMouseLeave={() => setHide(true)}
                     style={{aspectRatio: `${hidden ? '16 / 9' : ''}`}}
                >
                    <div className={`${styles.overlap2} ${hide ? styles.hide : ''}`}>
                        <p className={styles.p}>Теорія 2: Lorem ipsum dolor sim loperic</p>
                    </div>
                    <img src={`https://drive.google.com/uc?export=view&id=${documents[current]['fileId']}`}
                         alt={'pdf'} className={styles.image}/>
                    <div className={`${styles.overlapGroup} ${hide ? styles.hide : ''}`}>
                        <div className={styles.div}>
                            <div className={styles.div2}>
                                <img
                                    className={styles.faArrowUp}
                                    onClick={() => setCurrent(current === 0 ? 0 : current - 1)}
                                    alt="Fa arrow up"
                                    src="https://c.animaapp.com/CrFN0nDX/img/fa-arrow-up-5.svg"
                                />
                                <div className={styles.textWrapper}>{current + 1} / {max + 1}</div>
                                <img
                                    className={styles.faArrowUp}
                                    onClick={() => setCurrent(current === max ? max : current + 1)}
                                    alt="Fa arrow up"
                                    src="https://c.animaapp.com/CrFN0nDX/img/fa-arrow-up-4.svg"
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default DocumentComponent;