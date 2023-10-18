import './course-page.css';
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import {useContext, useEffect, useMemo, useState} from "react";
import VideoComponent from "../../components/video-component/VideoComponent";
import DocumentComponent from "../../components/document-component/DocumentComponent";
import TestComponent from "../../components/test-component/TestComponent";
import CourseWrapper from "../../components/course-wrapper/CourseWrapper";
import {GonnaLearn, Reviews} from "../../components/courses-components/CoursesComponents";
import {useNavigate, useParams} from "react-router-dom";
import {apiEndpoint} from "../../api";
import {launchError, LoadingContext} from "../../components/layout/Layout";

export const getStars = (count) => {
    const stars = [];
    for (let i = 0; i < 5; i++) {
        if (i < count) {
            stars.push(<img className="star" alt="Star" key={'star' + i}
                            src="https://c.animaapp.com/5wTdv0to/img/vector.svg"/>)
        } else {
            stars.push(<img className="star" alt="Star" key={'star' + i}
                            src="https://c.animaapp.com/QhKYdRQi/img/vector.svg"/>)
        }
    }
    return stars;
}

const CoursePage = () => {
    const navigate = useNavigate();
    const [show, setShow] = useState(true);

    const setLoading = useContext(LoadingContext);
    const {id} = useParams();
    const [course, setCourse] = useState(null);
    const [enrollment, setEnrollment] = useState(null);
    const [currentLesson, setCurrentLesson] = useState(null);

    const scrollBar = ({target}) => {
        const percentHeight = 612 * 612 / target.scrollHeight;

        const scroll = target.scrollTop;
        const height = target.scrollHeight - target.clientHeight;
        const scrolled = (scroll / height) * 100;

        const res = (612 - percentHeight) / 100 * scrolled;
        const element = document.getElementById('scroll');

        element.style.top = `${res}px`;
        element.style.height = `${612 * 612 / target.scrollHeight}px`;
    }

    useEffect(() => {
        setLoading(true);

        apiEndpoint(`course/get/${id}`)
            .fetch()
            .then(res => {
                setLoading(false);
                if (res.data === null) {
                    navigate('/');
                    return;
                }
                if (res.data.navigate) {
                    navigate(res.data.navigate);
                    return;
                }
                setCourse(res.data.course);
                setEnrollment(res.data['enrollment']);
                setCurrentLesson(res.data.lesson);
            })
            .catch(() => {
                setLoading(false);
                navigate('/');
            });
    }, []);

    useEffect(() => {
        if (!course) return;

        const scrollHeight = document.getElementById('scrollElement').scrollHeight;
        document.getElementById('scroll').style.height = `${612 * 612 / scrollHeight}px`;
    }, [course]);

    const switchLessonType = (lesson) => {
        let testData = enrollment['completedLessons'].filter(c => c['lessonId'] === lesson.id);
        if (testData.length > 0)
            testData = testData[0];
        else testData = null;

        switch (lesson.type.toLowerCase()) {
            case 'video':
                return <VideoComponent hidden={!show} video={lesson.video} fullScreen={show} courseId={course.id}
                                       show={!enrollment['completed']}/>;
            case 'document':
                return <DocumentComponent hidden={!show} documents={lesson['documents']} fullScreen={show}
                                          courseId={course.id} show={!enrollment['completed']}/>;
            case 'quiz':
                return <TestComponent hidden={!show} questions={lesson['questions']} courseId={course.id}
                                      show={!enrollment['completed']}
                                      testData={testData ? testData['quizAnswers'] : null}
                                      mark={testData ? testData['testPercentage'] : null}/>;
            default:
                break;
        }
    }

    if (!course) {
        return <></>;
    }

    return (<div>
        <Header/>
        <div className="course-page-video">
            <div className="div-4">
                <div className="overlap-2">
                    {show && <p className="text-wrapper-28">{course.name}</p>}
                    <div className={'display-wrapper'}>
                        <div className={'display'}>
                            {switchLessonType(currentLesson)}
                        </div>
                        {!show && <div className={'play-button'} onClick={() => setShow(true)}>
                            <img
                                className={'ic-round-arrow-back'}
                                alt="Ic round arrow back"
                                src="https://c.animaapp.com/wJbpMdU5/img/ic-round-arrow-back-1.svg"
                            />
                            <img
                                className="ic-round-arrow-back-not"
                                alt="Ic round arrow back"
                                src="https://c.animaapp.com/GhuyHFnO/img/ic-round-arrow-back-1.svg"
                            />
                            <div className="text-wrapper">Матеріали курсу</div>
                        </div>}
                        <div className={`materials ${show ? '' : 'hide'}`}>
                            <div className={'title'} style={{gap: 20}}>
                                <div className="text-wrapper-29" onClick={() => setShow(false)}>
                                    Матеріали курсу
                                </div>
                                <img
                                    className="ic-round-arrow-back"
                                    alt="Ic round arrow back"
                                    src="https://c.animaapp.com/GuRgT161/img/ic-round-arrow-back.svg"
                                />
                            </div>
                            <div className={'material-wrap'}>
                                <div className={`property-frame-wrapper frame-95`} onScroll={e => scrollBar(e)}
                                     id={'scrollElement'}>
                                    {course['sessions'].map((section, index) => (
                                        <PropertyFrameWrapper key={'Section' + index}
                                                              completedLessons={enrollment['completedLessons']}
                                                              currentSession={enrollment['currentSessionId']}
                                                              currentLessonId={enrollment['currentLessonId']}
                                                              data={section} index={index + 1}
                                                              setCurrentLesson={setCurrentLesson}/>))}
                                </div>
                                <div className="rectangle-wrapper">
                                    <div className="rectangle-2" id={'scroll'}/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <GonnaLearn sessions={course['sessions']}/>
                </div>
                <Reviews course={course}/>
                <CourseWrapper teacherId={course.teacherId} courseId={course.id}/>
            </div>
        </div>
        <Footer/>
    </div>);
}

const PropertyFrameWrapper = ({data, index, completedLessons, currentSession, setCurrentLesson, currentLessonId}) => {
    const [opened, setOpened] = useState(data.id === currentSession);

    const PropertyDefault = ({lesson, number}) => {
        const getImage = useMemo(() => {
            switch (lesson.type.toLowerCase()) {
                case 'video':
                    return "https://c.animaapp.com/GuRgT161/img/ph-video-light-7.svg";
                case 'document':
                    return "https://c.animaapp.com/GuRgT161/img/ph-file-7.svg";
                case 'quiz':
                    return "https://c.animaapp.com/GuRgT161/img/fluent-task-list-square-ltr-20-regular-3.svg";
                default:
                    break;
            }
        }, [lesson])

        const getText = useMemo(() => {
            switch (lesson.type.toLowerCase()) {
                case 'video':
                    if (!lesson.video.duration)
                        apiEndpoint('course/update-video').post(lesson.video.id)
                            .catch(err => launchError(err));

                    return `${lesson.video.duration} хвили${lesson.video.duration === 1 ? 'на' : (lesson.video.duration < 5 ? 'ни' : 'н')}`;
                case 'document':
                    return `${lesson['documents']?.length ?? 0} ${lesson['documents']?.length < 5 ? `сторінк${lesson['documents']?.length === 1 ? 'а' : 'и'}` : 'сторінок'}`;
                case 'quiz':
                    return `${lesson['questions']?.length ?? 0} питан${lesson['questions']?.length < 5 ? 'ня' : 'ь'}`
                default:
                    break;
            }
        }, [lesson])

        return (<div className="group-6">
                <div className="group-7">
                    <div className="div-2">
                        {completedLessons.map(c => c['lessonId']).includes(lesson.id) &&
                            <img className="uilCheck" alt="Uil check"
                                 src="https://c.animaapp.com/5ThCsLyF/img/uil-check.svg"/>}
                    </div>
                    <div className="text-wrapper-4"
                         onClick={() => (completedLessons.map(c => c['lessonId']).includes(lesson.id) || currentLessonId === lesson.id) && setCurrentLesson(lesson)}>
                        {number}. {lesson.name}
                    </div>
                </div>
                <div className="group-8">
                    <img
                        className="ph-video-light"
                        alt="Ph video light"
                        src={getImage}
                    />
                    <div className="text-wrapper-5">{getText}</div>
                </div>
            </div>
        );
    };

    return (<>
        <div className="overlap-group">
            <div className="element-course">
                <p className="element-course-2">
                    <span className="span">Розділ {index}:</span>
                    <span className="text-wrapper-2"> {data.name}</span>
                </p>
                <img
                    className="ic-round-navigate"
                    alt="Ic round navigate"
                    onClick={() => setOpened(!opened)}
                    src={opened ? "https://c.animaapp.com/GuRgT161/img/ic-round-navigate-next-14.svg" : "https://c.animaapp.com/GuRgT161/img/ic-round-navigate-next-17.svg"}
                />
            </div>
            <div className="div">
                {completedLessons?.filter(c => c.lesson.sessionId === data.id).length ?? 0}/{data['lessons']?.length ?? 0} -
                пройдено
            </div>
            {opened && <div>
                <div className={'separate-line'}/>
                <div className={'group'}>
                    {data['lessons'].map((lesson, index) => (<PropertyDefault key={'Lesson' + index} lesson={lesson}
                                                                              number={index + 1}/>))}
                </div>
            </div>}
        </div>
    </>)
}

export default CoursePage;
