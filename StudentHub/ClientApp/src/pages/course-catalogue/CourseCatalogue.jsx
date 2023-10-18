import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import styles from './course-catologue.module.css';
import {Courses} from "../../components/course-wrapper/CourseWrapper";
import {useEffect, useState} from "react";
import {apiEndpoint} from "../../api";

const Options = ({data, setFilter, state, filterName}) => {
    const handleClick = (value, index) => {
        if (filterName === 'Free' || filterName === 'Certification')
            setFilter({...state, [filterName]: index === 0});

        else
            setFilter({...state, categoryId: value.id});
    }

    const isSelected = (value, index) => {
        if (filterName === 'Free' || filterName === 'Certification')
            return state[filterName] === (index === 0);

        return state.categoryId === value.id;
    }

    return (
        <>
            {
                data.map((value, index) =>
                    <div className={styles.wrapper} key={value.id ?? value} onClick={() => handleClick(value, index)}>
                        <div className={styles.rectangle}>
                            {isSelected(value, index) && <img className={styles.uilCheck} alt="Uil check"
                                                              src="https://c.animaapp.com/5ThCsLyF/img/uil-check.svg"/>}
                        </div>
                        <div className={styles.textWrapper17}>{value.name ?? value}</div>
                    </div>
                )}
        </>
    )
}

const CourseCatalogue = () => {
    const [themes, setThemes] = useState([]);
    const [data, setData] = useState([]);
    const [filter, setFilter] = useState({});

    useEffect(() => {
        apiEndpoint('filter').fetch()
            .then(res => setThemes(res.data)).catch(err => console.log(err));
    }, []);

    useEffect(() => {
        apiEndpoint('filter').post(filter)
            .then(res => setData(res.data)).catch(err => console.log(err));
    }, [filter]);

    return (
        <div>
            <Header/>
            <div className={styles.coursesCatalogue}>
                <div className={styles.coursesCatalogueV}>
                    <div className={styles.overlap4}>
                        <div className={styles.group15}>
                            <div className={styles.textWrapper43}>Каталог курсів</div>
                            <p className={styles.textWrapper42}>Знайди будь-який курс який тобі потрібен!</p>
                        </div>
                    </div>
                    <div className={styles.group7}>
                        <div className={styles.group8}>
                            <div className={styles.textWrapper12}>Фільтрувати</div>
                            <div className={styles.sort}>
                                <div className={styles.textWrapper13}>Сортувати</div>
                                <div className={styles.frame4}>
                                    <div className={styles.textWrapper14}>Спочатку нові</div>
                                    <img
                                        className={styles.icRoundNavigate}
                                        alt="Ic round navigate"
                                        src="https://c.animaapp.com/vAwxSWNV/img/ic-round-navigate-next.svg"
                                    />
                                </div>
                            </div>
                            <div className={styles.frame5}>
                                <img
                                    className={styles.tablerSearch2}
                                    alt="Tabler search"
                                    src="https://c.animaapp.com/vAwxSWNV/img/tabler-search-1.svg"
                                />
                                <div className={styles.textWrapper15}>Пошук за назвою</div>
                            </div>
                        </div>
                        <div className={styles.split}>
                            <div className={styles.overlapGroupWrapper}>
                                <div className={styles.overlapGroup}>
                                    <div className={styles.textWrapper16}>Теми</div>
                                    <div className={styles.group9}>
                                        <Options data={themes} state={filter} setFilter={setFilter}
                                                 filterName={'Themes'}/>
                                    </div>
                                    <div className={styles.textWrapper16}>Сертифікація</div>
                                    <div className={styles.group9}>
                                        <Options data={['З сертифікатом', 'Без сертифіката']}
                                                 state={filter} setFilter={setFilter} filterName={'Certification'}/>
                                    </div>
                                    <div className={styles.textWrapper16}>Ціна</div>
                                    <div className={styles.group9}>
                                        <Options data={['Безкоштовні', 'Платні']}
                                                 state={filter} setFilter={setFilter} filterName={'Free'}/>
                                    </div>
                                    <div className={styles.price}>
                                        <div className={styles.overlap}>
                                            <div className={styles.textWrapper36}>0</div>
                                        </div>
                                        <img className={styles.vector2} alt="Vector"
                                             src="https://c.animaapp.com/vAwxSWNV/img/vector-57.svg"/>
                                        <div className={styles.overlap}>
                                            <div className={styles.textWrapper37}>10000</div>
                                        </div>
                                        <div className={styles.divWrapper}>
                                            <div className={styles.textWrapper38}>ОК</div>
                                        </div>
                                    </div>
                                    <div className={styles.overlap3}>
                                        <div className={styles.ellipse}/>
                                        <img className={styles.vector3} alt="Vector"
                                             src="https://c.animaapp.com/vAwxSWNV/img/vector-58.svg"/>
                                        <div className={styles.ellipse2}/>
                                    </div>
                                    <div>
                                        <div className={styles.textWrapper16}>Рейтинг</div>
                                        <img className={styles.group12} alt="Group"
                                             src="https://c.animaapp.com/vAwxSWNV/img/group-143@2x.png"/>
                                    </div>
                                    <div className={styles.textWrapper35}
                                         onClick={() => setFilter({})}>
                                        Відмінити фільтри
                                    </div>
                                </div>
                            </div>
                            {data.length === 0 ? <div className={styles.group13}>
                                <div className={styles.center}>
                                    <img
                                        className={styles.tdesignSearchError}
                                        alt="Tdesign search error"
                                        src="https://c.animaapp.com/vAwxSWNV/img/tdesign-search-error.svg"
                                    />
                                </div>
                                <div className={styles.group14}>
                                    <p className={styles.textWrapper39}>
                                        На жаль, ми не змогли нічого знайти за вашим запитом
                                    </p>
                                    <p className={styles.textWrapper40}>
                                        Спробуйте оптимізувати пошук. Ось кілька пропозицій:
                                    </p>
                                    <ul className={styles.textWrapper41}>
                                        <li>Переконайтеся, що всі слова написані правильно;</li>
                                        <li>Спробуйте ввести інший пошуковий запит;</li>
                                        <li>Спробуйте використати більш загальні пошукові запити;</li>
                                    </ul>
                                </div>
                            </div> : <div className={styles.courses}><Courses courses={data}/></div>}
                        </div>
                    </div>
                </div>
            </div>
            <Footer/>
        </div>
    )
}

export default CourseCatalogue;