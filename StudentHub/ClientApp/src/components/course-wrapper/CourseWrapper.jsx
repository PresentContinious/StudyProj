import styles from './course-wrapper.module.css';
import {getStars} from "../../pages/course-page/CoursePage";
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import {apiEndpoint} from "../../api";

const CourseWrapper = ({teacherId, courseId}) => {
    const [courses, setCourses] = useState([]);
    const [teacher, setTeacher] = useState({});

    useEffect(() => {
        apiEndpoint(`course/get-teacher-courses/${teacherId}/${courseId}`)
            .fetch()
            .then(res => {
                setCourses(res.data['courses']);
                setTeacher(res.data.teacher);
            })
            .catch(err => console.log(err));
    }, [teacherId, courseId]);

    return (
        <div className={styles.group62}>
            <p className={styles.marynaSchevchuk}>
                <span className={styles.textWrapper38}>Інші курси від </span>
                <span className={styles.textWrapper39}>{teacher.fullName}</span>
            </p>
            <div className={styles.courses}>
                <Courses courses={courses}/>
            </div>
        </div>
    )
}

export const Courses = ({courses = []}) => {
    const navigate = useNavigate();

    return (
        <>
            {
                courses.map(course =>
                    <div className={styles.courseObject} key={'CourseOfCourse' + course.id + Math.random()}>
                        <div className={styles.courseImage}>
                            <img className={styles.rectangle} alt="Rectangle"
                                 src={course['fileId'] ? `https://drive.google.com/uc?export=view&id=${course['fileId']}` : "https://c.animaapp.com/Y2ARkupF/img/rectangle-75.png"}/>
                        </div>
                        <div className={styles.courseFrame}>
                            <div className={styles.textWrapper}>
                                {course['price'] ?? 'Безкоштовно'} {course['price'] && '₴'}
                            </div>
                        </div>
                        <div className={styles.courseBox}>
                            <div className={styles.overlap}>
                                <div className={styles.textWrapper}>{course.name}</div>
                                <div className={styles.div}>{course.teacher?.fullName}</div>
                                <div className={styles.overlapGroupWrapper}>
                                    <div className={styles.overlapGroup}>
                                        <div className={styles.textWrapper2}>{course['rating']}</div>
                                        {getStars(course['rating'])}
                                    </div>
                                </div>
                                <div className={styles.frameWrapper}>
                                    <div className={styles.notFrame}
                                         onClick={() => {
                                             navigate('/view-course/' + course.id);
                                             navigate(0);
                                         }}>
                                        <div className={styles.textWrapper3}>Обрати курс</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )
            }
        </>
    )
}

export default CourseWrapper;