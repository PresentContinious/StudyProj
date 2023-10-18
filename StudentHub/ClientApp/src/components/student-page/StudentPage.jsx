import styles from './student-page.module.css';
import {Courses} from "../course-wrapper/CourseWrapper";
import MainPageOutro from "../main-page-outro/MainPageOutro";
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import {apiEndpoint} from "../../api";

const StudentPage = () => {
    const navigate = useNavigate();
    const [data, setData] = useState({newCourses: [], popularCourses: []});

    const themes = [
        {name: 'IT', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-8@2x.png'},
        {name: 'Графічний Дизайн', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-6@2x.png'},
        {name: 'Бізнес', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-4@2x.png'},
        {name: 'Іноземні мови', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-2@2x.png'},
        {name: 'Право', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-7@2x.png'},
        {name: 'Психологія', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-5@2x.png'},
        {name: 'Особистий розвиток', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-3@2x.png'},
        {name: 'Культура', img: 'https://c.animaapp.com/YTRvJR1K/img/rectangle-75-1@2x.png'}
    ]

    useEffect(() => {
        apiEndpoint('course/statistics').fetch()
            .then(res => setData(res.data))
            .catch(err => console.log(err));
    }, []);

    return (
        <>
            <div className={styles.studentMainPage}>
                <div className={styles.studentMainPageV}>
                    <div className={styles.overlap4}>
                        <div></div>
                        <img className={styles.image} alt="Frozen"
                             src="https://c.animaapp.com/YTRvJR1K/img/-----------------2.png"/>
                        <div className={styles.rectangle2}>
                            <div className={styles.group26}>
                                <div className={styles.group27}>
                                    <div className={styles.textWrapper20}>80 000</div>
                                    <div className={styles.textWrapper21}>активних слухачів</div>
                                </div>
                                <img className={styles.vector5} alt="Vector"
                                     src="https://c.animaapp.com/YTRvJR1K/img/vector-54.svg"/>
                                <div className={styles.group27}>
                                    <div className={styles.textWrapper22}>1000+</div>
                                    <div className={styles.textWrapper21}>онлайн-курсів</div>
                                </div>
                                <img className={styles.vector5} alt="Vector"
                                     src="https://c.animaapp.com/YTRvJR1K/img/vector-54.svg"/>
                                <div className={styles.group27}>
                                    <div className={styles.textWrapper22}>60 000</div>
                                    <div className={styles.textWrapper21}>виданих сертифікатів</div>
                                </div>
                                <img className={styles.vector5} alt="Vector"
                                     src="https://c.animaapp.com/YTRvJR1K/img/vector-54.svg"/>
                                <div className={styles.group27}>
                                    <div className={styles.textWrapper22}>2</div>
                                    <div className={styles.textWrapper21}>роки у сфері освіти</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <SuggestCourses title={"Нові курси"} data={data.newCourses}/>
                    <div className={styles.overlap9}>
                        <div className={styles.group43}>
                            <div className={styles.group44}>
                                <img className={styles.img3} alt="Ion home"
                                     src="https://c.animaapp.com/YTRvJR1K/img/ion-home.svg"/>
                                <p className={styles.textWrapper50}>Отримайте необхідні навички за допомогою понад 100
                                    000 відеокурсів</p>
                            </div>
                            <div className={styles.group44}>
                                <img className={styles.antDesignStar} alt="Ant design star"
                                     src="https://c.animaapp.com/YTRvJR1K/img/ant-design-star-filled.svg"/>
                                <p className={styles.textWrapper52}>Обирайте курси, які викладають реальні експерти</p>
                            </div>
                            <div className={styles.group44}>
                                <img className={styles.img3} alt="Ph video fill"
                                     src="https://c.animaapp.com/YTRvJR1K/img/ph-video-fill.svg"/>
                                <p className={styles.textWrapper51}>
                                    Навчайтеся у своєму власному темпі з постійним доступом на мобільному та комп’ютері
                                </p>
                            </div>
                        </div>
                    </div>
                    <SuggestCourses title={"Популярні курси"} data={data.popularCourses}/>
                    <div className={styles.partners}>
                        <div className={styles.textWrapper49}>Наші клієнти та партнери</div>
                        <div className={styles.overlap8}>
                            {[1, 2, 3, 4, 5, 6, 7, 8, 9].map(item =>
                                (
                                    <div className={styles.group41} key={item}>
                                        <div className={styles.rectangle4}>
                                            <img
                                                className={styles.ItStepSchool}
                                                alt="It STEP SCHOOL"
                                                src="https://c.animaapp.com/YTRvJR1K/img/it-step-school-1@2x.png"
                                            />
                                        </div>
                                    </div>
                                ))}
                        </div>
                    </div>
                    <div className={styles.overlap2}>
                        <div className={styles.frame5}>
                            <div className={styles.textWrapper15}>Наші викладачі</div>
                            <p className={styles.textWrapper14}>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet,
                                consectetur adipiscing
                                elit.Lorem ipsum dolor sit amet, consectetur.
                            </p>
                            <div className={styles.icons}>
                                <div className={styles.group23}>
                                    <img
                                        className={styles.iconOirVerified}
                                        alt="Iconoir verified"
                                        src="https://c.animaapp.com/YTRvJR1K/img/iconoir-verified-user.svg"
                                    />
                                    <div className={styles.textWrapper16}>Усі викладачі сертифіковані</div>
                                </div>
                                <div className={styles.group23}>
                                    <img
                                        className={styles.solarDiploma}
                                        alt="Solar diploma"
                                        src="https://c.animaapp.com/YTRvJR1K/img/solar-diploma-verified-broken.svg"
                                    />
                                    <div className={styles.textWrapper16}>Усі курси перевірені</div>
                                </div>
                                <div className={styles.group23}>
                                    <img
                                        className={styles.solarVerifiedCheck}
                                        alt="Solar verified check"
                                        src="https://c.animaapp.com/YTRvJR1K/img/solar-verified-check-broken.svg"
                                    />
                                    <div className={styles.textWrapper16}>Усі платежі застраховані</div>
                                </div>
                            </div>
                            <div>
                                <div className={styles.component}>
                                    <div className={styles.textWrapper}>Оформити підписку на викладача</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className={styles.overlap3}>
                        <div className={styles.textWrapper19}>Популярні теми</div>
                        <div className={styles.wrapper}>
                            {themes.map((item, index) => (
                                <div className={styles.group42} key={index}
                                     style={{
                                         background: `url('${item.img}')`,
                                         backgroundSize: 'cover',
                                         backgroundPosition: '50% 50%'
                                     }}>
                                    <div className={styles.blur}>
                                        <div className={styles.textWrapper1}>{item.name}</div>
                                    </div>
                                </div>
                            ))}
                        </div>
                        <div className={styles.center}>
                            <div className={styles.component} onClick={() => navigate('/catalogue')}>
                                <div className={styles.textWrapper}>Перейти до каталогу курсів</div>
                                <img alt="Vector"
                                     src="https://c.animaapp.com/YTRvJR1K/img/vector-60-3.svg"/>
                            </div>
                        </div>
                    </div>
                    <MainPageOutro/>
                </div>
            </div>
        </>
    )
}

const SuggestCourses = ({title, data}) => {
    const navigate = useNavigate();

    return (
        <div className={styles.wrap}>
            <div className={styles.group47}>
                <div className={styles.textWrapper53}>{title}</div>
                <div className={styles.frame} onClick={() => navigate('/catalogue')}>
                    <div className={styles.textWrapper13}>Всі курси</div>
                </div>
            </div>
            <div className={styles.courses}>
                <Courses courses={data}/>
            </div>
        </div>
    )
}

export default StudentPage;