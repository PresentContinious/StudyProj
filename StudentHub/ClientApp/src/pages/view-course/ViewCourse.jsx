import styles from './view-course.module.css';
import Footer from "../../components/footer/Footer";
import Header from "../../components/header/Header";
import CourseWrapper from "../../components/course-wrapper/CourseWrapper";
import {useContext, useEffect, useState} from "react";
import {GonnaLearn, Reviews} from "../../components/courses-components/CoursesComponents";
import {useNavigate, useParams} from "react-router-dom";
import {apiEndpoint} from "../../api";
import {AuthContext, LoadingContext} from "../../components/layout/Layout";

const ViewCourse = () => {
    const {id} = useParams();
    const navigate = useNavigate();
    const [course, setCourse] = useState(null);
    const [enrollment, setEnrollment] = useState(null);
    const {loggedIn} = useContext(AuthContext);
    const setLoading = useContext(LoadingContext);

    useEffect(() => {
        apiEndpoint('course/get-view/' + id).fetch()
            .then(res => {
                setCourse(res.data.course);
                setEnrollment(res.data['studentCourse']);
            }).catch(err => console.log(err));
    }, [id]);

    const handleClick = () => {
        if (!loggedIn) {
            navigate('/sign-in');
            return;
        }

        if (enrollment?.['paid']) {
            navigate('/course/' + id);
            return;
        }

        navigate('/pay/' + id);
    }

    setLoading(true);
    if (!course) return <></>;
    setLoading(false);

    return (
        <>
            <Header/>
            <div className={styles.coursePage}>
                <div className={styles.coursePageV}>
                    <div className={styles.overlap}>
                        <div className={styles.closeUpHand}>
                            <div className={styles.split}>
                                <div className={styles.left}>
                                    <div className={styles.leftGap}>
                                        <p className={styles.element}>{course.name}</p>
                                        <p className={styles.textWrapper49}>
                                            {course.description}
                                        </p>
                                        <p className={styles.marynaSchevchuk}>
                                            <span className={styles.span}>Автор: </span>
                                            <span className={styles.textWrapper50}>{course.teacher.fullName}</span>
                                        </p>
                                    </div>
                                    <div className={styles.wrapper}>
                                        {enrollment?.['paid'] ?
                                            <div className={styles.progress}>
                                                <div className={styles.titleText}>
                                                    {enrollment['completed'] ? 'Вітаємо! Ви закінчили цей курс' : 'Прогрес курсу'}
                                                </div>
                                                <div className={styles.progressBar}>
                                                    <div className={styles.rectangleBar}>
                                                        <div className={styles.progressYellow}
                                                             style={{width: enrollment.progress + '%'}}/>
                                                    </div>
                                                    <div
                                                        className={styles.littleText}>{enrollment['completed'] ? 'Завершено' : `${enrollment.progress}%`}</div>
                                                </div>
                                            </div> :
                                            <p className={styles.element2}>
                                                <span className={styles.span}>Вартість: </span>
                                                <span className={styles.textWrapper50}>
                                                    {course['price'] ?? 'Безкоштовно'}{course['price'] && '₴'}
                                                </span>
                                            </p>
                                        }
                                        <div className={styles.component} onClick={handleClick}>
                                            {enrollment?.['completed'] ?
                                                <div className={styles.textWrapper}>
                                                    {'Переглянути курс'}
                                                </div> : <div className={styles.textWrapper}>
                                                    {enrollment?.['paid'] ? 'Продовжити навчання' : 'Придбати курс'}
                                                </div>}
                                        </div>
                                    </div>
                                </div>
                                <div className={styles.propertyGroupWrapper}>
                                    <div className={styles.overlapGroup}>
                                        <div className={styles.rectangle}>
                                            <img className={styles.phPlayFill} alt="Ph play fill"
                                                 src={'https://c.animaapp.com/5quB9onC/img/ph-play-fill-1.svg'}/>
                                            <div className={styles.textWrapper3}>Попередній перегляд курсу</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className={styles.rectangle5}>
                                <div className={styles.group23}>
                                    <div className={styles.group24}>
                                        <div className={styles.textWrapper51}>Оцінка: {course['rating']}</div>
                                        <div className={styles.textWrapper52}>відгуків: {course['totalReviews']}</div>
                                    </div>
                                    <img className={styles.vector2} alt="Vector"
                                         src="https://c.animaapp.com/5quB9onC/img/vector-55.svg"/>
                                    <div className={styles.group24}>
                                        <div className={styles.textWrapper51}>Тривалість: 5 тижнів</div>
                                        <div className={styles.textWrapper52}>навчайтеся у зручному темпі</div>
                                    </div>
                                    <img className={styles.vector2} alt="Vector"
                                         src="https://c.animaapp.com/5quB9onC/img/vector-55.svg"/>
                                    <div className={styles.group24}>
                                        <div className={styles.textWrapper51}>Рівень: Середній</div>
                                        <p className={styles.textWrapper52}>навантаження - 4-5 годин на добу</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className={styles.gonnaLearn}>
                        <GonnaLearn sessions={course.sessions}/>
                    </div>
                    <div className={styles.overlap4}>
                        <p className={styles.textWrapper62}>Цей курс для вас, якщо ви хочете…</p>
                        <div className={styles.gap}>
                            <div className={styles.overlap5}>
                                <img
                                    className={styles.solarArrowUp}
                                    alt="Solar arrow up"
                                    src="https://c.animaapp.com/5quB9onC/img/solar-arrow-up-linear-3.svg"
                                />
                                <p className={styles.textWrapper63}>Захистити заощадження від інфляції в період
                                    кризи</p>
                            </div>
                            <div className={styles.overlap5}>
                                <img
                                    className={styles.solarArrowUp}
                                    alt="Solar arrow up"
                                    src="https://c.animaapp.com/5quB9onC/img/solar-arrow-up-linear-3.svg"
                                />
                                <p className={styles.textWrapper63}>
                                    Навчитися вигідно інвестувати на світовому фондовому ринку
                                </p>
                            </div>
                            <div className={styles.overlap5}>
                                <img
                                    className={styles.solarArrowUp}
                                    alt="Solar arrow up"
                                    src="https://c.animaapp.com/5quB9onC/img/solar-arrow-up-linear-3.svg"
                                />
                                <p className={styles.textWrapper63}>Оптимізувати оподаткування інвестицій і прибутку</p>
                            </div>
                            <div className={styles.overlap5}>
                                <img
                                    className={styles.solarArrowUp}
                                    alt="Solar arrow up"
                                    src="https://c.animaapp.com/5quB9onC/img/solar-arrow-up-linear-3.svg"
                                />
                                <p className={styles.textWrapper63}>
                                    Спланувати фінансову свободу та наповнити її змістом
                                </p>
                            </div>
                        </div>
                    </div>
                    <Reviews/>
                    <div className={styles.pick}>
                        <p className={styles.textWrapper64}>Чому варто обрати цей курс?</p>
                        <div className={styles.group}>
                            {[0, 1, 2, 3, 4, 5].map(item =>
                                <div className={styles.box} key={item}>
                                    <img
                                        alt="Solar document add"
                                        src="https://c.animaapp.com/qCOTd5OH/img/solar-document-add-linear.svg"
                                    />
                                    <div className={styles.div}>
                                        <div className={styles.textWrapper1}>Навчання через практику</div>
                                        <p className={styles.p}>
                                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam pulvinar leo
                                            eu ante vulputate mattis. Sed
                                            tincidunt nunc fermentum pulvinar sodales.
                                        </p>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>
                    <div className={styles.textWrapper65}>Програма курсу</div>
                    <div className={styles.program}>
                        {
                            course.sessions.map((session, index) =>
                                <WrapOption key={session.id} number={index + 1} session={session}/>
                            )
                        }
                    </div>
                    <CourseWrapper teacherId={course.teacherId} courseId={course.id}/>
                </div>
            </div>
            <Footer/>
        </>
    )
}

const WrapOption = ({number, session}) => {
    const [show, setShow] = useState(false);

    return (
        <>
            <div className={styles.option}>
                <div className={styles.title} onClick={() => setShow(!show)}>
                    <div className={styles.titleWrap}>
                        <div className={styles.textWrapper2}>{number}</div>
                        <p className={styles.text}>{session.name}</p>
                    </div>
                    <img
                        style={{transform: show ? 'rotate(0)' : 'rotate(180deg)'}}
                        alt="Ic round navigate"
                        src={'https://c.animaapp.com/kEjaFOLW/img/ic-round-navigate-next.svg'}
                    />
                </div>
                {show &&
                    <div className={styles.body}>
                        {
                            session['lessons'].map((item, index) =>
                                <div key={'lessons' + item.id} className={styles.list}>
                                    <div className={styles.num}>{number}.{index + 1}</div>
                                    <p className={styles.subTitle}>{item.name}</p>
                                </div>
                            )
                        }
                    </div>
                }
            </div>
        </>
    )
}

export default ViewCourse;