import styles from './my-courses.module.css';
import Footer from "../../components/footer/Footer";
import Header from "../../components/header/Header";
import {useEffect, useMemo, useState} from "react";
import {getStars} from "../course-page/CoursePage";
import {apiEndpoint} from "../../api";
import {useNavigate} from "react-router-dom";

const MyCourses = () => {
    const [activeTab, setActiveTab] = useState(0);
    const [data, setData] = useState([]);

    useEffect(() => {
        apiEndpoint('user/courses').fetch()
            .then(res => setData(res.data))
            .catch(err => console.log(err))
    }, []);

    const getCourses = useMemo(() => {
        if (activeTab === 1) return data.filter(item => !item['completed']);
        if (activeTab === 2) return data.filter(item => item['completed']);

        return data;
    }, [activeTab, data])

    return (
        <div>
            <Header/>
            <div className={styles.studentCourses}>
                <div className={styles.div2}>
                    <div className={styles.overlapGroup}>
                        <div className={styles.textWrapper19}>Мої курси</div>
                        <p className={styles.textWrapper18}>Почніть здобувати нові знання вже сьогодні!</p>
                    </div>
                    <div className={styles.group6}>
                        <div className={styles.wrap}>
                            {
                                ['Усі курси', 'В процесі', 'Закінчені'].map((item, index) => (
                                    <div
                                        key={item + index}
                                        className={`${styles.textWrapper11} ${activeTab === index ? styles.active : ''}`}
                                        onClick={() => setActiveTab(index)}>
                                        {item}
                                    </div>
                                ))
                            }
                        </div>
                        <div className={styles.frame3}>
                            <img
                                className={styles.tablerSearch}
                                alt="Tabler search"
                                src="https://c.animaapp.com/CusQsmRv/img/tabler-search-1.svg"
                            />
                            <div className={styles.textWrapper14}>Пошук за назвою</div>
                        </div>
                    </div>
                    {
                        getCourses.length === 0 ? <div className={styles.center}>
                            <p className={styles.textWrapper15}>
                                Почніть здобувати нові знання з більш ніж 100 000 курсів
                                вже сьогодні!
                            </p>
                            <div className={styles.flex}>
                                <p className={styles.textWrapper17}>Після покупки курс відобразиться тут.</p>
                                <div className={styles.textWrapper16}>Перегляньте прямо зараз.</div>
                            </div>
                        </div> : <Courses data={getCourses}/>
                    }
                </div>
            </div>
            <Footer/>
        </div>
    )
}

const Courses = ({data}) => {
    const navigate = useNavigate();

    return (
        <div className={styles.wrapper}>
            {
                data.map((enrollment) => (
                    <div className={styles.box} key={enrollment.course.id + 'course'}>
                        <div className={styles.group}>
                            <img className={styles.rectangle} alt="Rectangle"
                                 src={enrollment.course['fileId'] ?
                                     `https://drive.google.com/uc?export=view&id=${enrollment.course['fileId']}`
                                     : "https://c.animaapp.com/bNP5uILv/img/rectangle-75.png"}/>
                            <div className={styles.overlap}>
                                <div className={styles.div}>{enrollment.course.name}</div>
                                <div className={styles.textWrapper2}>{enrollment.course.teacher?.fullName}</div>
                                <div className={styles.overlapGroupWrapper}>
                                    <div className={styles.textWrapper3}>{enrollment.course['rating']}</div>
                                    {getStars(enrollment.course['rating'])}
                                </div>
                                <div className={styles.group2}>
                                    <div className={styles.textWrapper4}>Прогрес курсу</div>
                                    <div className={styles.group3}>
                                        <div className={styles.rectangleWrapper}>
                                            <div className={styles.rectangle2}
                                                 style={{width: enrollment.progress + '%'}}/>
                                        </div>
                                        <div className={styles.textWrapper5}>{enrollment.progress}%</div>
                                    </div>
                                </div>
                                <div className={styles.component}>
                                    <div className={styles.frame}
                                         onClick={() => navigate(`/course/${enrollment.course.id}`)}>
                                        <div className={styles.textWrapper}>Перейти до курсу</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ))
            }
        </div>
    )
}

export default MyCourses;