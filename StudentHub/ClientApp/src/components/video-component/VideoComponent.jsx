import styles from './video-component.module.css';
import {useState} from "react";
import {apiEndpoint} from "../../api";
import {useNavigate} from "react-router-dom";

export const NextButton = ({fullScreen, courseId}) => {
    const navigate = useNavigate();
    const [inactive, setInactive] = useState(false);

    const handleClick = () => {
        setInactive(true);
        apiEndpoint('course/next/' + courseId).post()
            .then(res => {
                if (res.data.navigate)
                    navigate(res.data.navigate);
                else
                    navigate(0)
            }).catch(err => {
            console.log(err);
            setInactive(false);
        });
    }

    return (
        <div className={`${styles.wrap} ${inactive && styles.inactive}`}
             style={fullScreen ? {top: '-15%'} : {bottom: '-10%'}}
             onClick={handleClick}>
            <div className={styles.button}>
                Наступний урок
            </div>
        </div>
    )
}

const VideoComponent = ({hidden, video, fullScreen, courseId, show}) => {
    const [hide, setHide] = useState(false);

    return (
        <div style={{position: 'relative'}}>
            {show && <NextButton fullScreen={fullScreen} courseId={courseId}/>}
            <div className={styles.frame4}>
                <div className={styles.overlap3}>
                    <div className={`${styles.overlap5} ${hide ? styles.hide : ''}`}>
                        <p className={styles.textWrapper37}>Відео: {video.name}</p>
                    </div>
                    <iframe src={`https://drive.google.com/file/d/${video['fileId']}/preview`} width="100%"
                            height={`${hidden ? '900px' : '685px'}`} allowFullScreen={true}
                            className={styles.iframe} id={'iframe'}
                            onMouseLeave={() => setHide(true)}
                            onMouseEnter={() => setHide(false)}>
                    </iframe>
                </div>
            </div>
        </div>
    )
}

export default VideoComponent;