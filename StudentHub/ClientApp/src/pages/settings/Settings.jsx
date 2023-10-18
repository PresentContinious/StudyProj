import styles from './settings.module.css';
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import {useNavigate} from "react-router-dom";
import {useContext, useEffect, useState} from "react";
import {LogoutModal} from "../../components/modal-windows/ModalWindows";
import {apiEndpoint} from "../../api";
import {launchInfo, LoadingContext} from "../../components/layout/Layout";

export const Multiline = ({label, value, setValue, width = '100%'}) => {
    return (
        <>
            <div className={styles.inputWrapper} style={{width: width}}>
                <textarea className={styles.input} placeholder={`${label}`} defaultValue={value}
                          rows={5}
                          onChange={(e) => setValue(e.target.value)}/>
            </div>
        </>
    )
}

export const Input = ({label, value, setValue, width = 'fit-content'}) => {
    return (
        <>
            <div className={styles.inputWrapper} style={{width: width}}>
                <input className={styles.input} placeholder={`${label}`} defaultValue={value}
                       onChange={(e) => setValue(e.target.value)}/>
            </div>
        </>
    )
}

export const Select = ({label, options, width = 'fit-content', gap = 10, value, setValue}) => {
    if (!value)
        value = " ";

    options ??= [];

    return (
        <>
            <div className={styles.inputWrapper} style={{width: width, gap: gap}}>
                <select className={styles.input} placeholder={label} value={value ?? " "}
                        style={{cursor: 'pointer'}}
                        onChange={(e) => setValue(e.target.value)}>
                    <option value={" "} disabled={true}>{label}</option>
                    {
                        options.map((option) => (
                            <option value={option.value || option.name || option}
                                    key={'select' + (option.name || option)}>
                                {option.name || option}
                            </option>)
                        )
                    }
                </select>
                <img
                    className={styles.icon}
                    alt="Ic round arrow back"
                    src="https://c.animaapp.com/0N8HhTem/img/ic-round-arrow-back-ios-8.svg"
                />
            </div>
        </>
    )
}

export const Radio = ({options, value, setValue}) => {
    return (
        <>
            {
                options.map((option) => (
                    <div className={styles.group21}
                         onClick={() => setValue(option.value)}
                         key={'option' + Math.random()}>
                        <div className={styles.ellipse3}>
                            {(value === option.value) &&
                                <div className={styles.ellipse4}/>}
                        </div>
                        <div className={styles.textWrapper42}>{option.label}</div>
                    </div>
                ))
            }
        </>
    )
}

export const NavMenu = ({src, label, ionIcon, onClick, selected}) => {
    return (
        <>
            <div className={`${styles.group30} ${selected && styles.selected}`} onClick={onClick}>
                {ionIcon ? ionIcon : <img
                    className={styles.img3}
                    alt="Pepicons pop leave"
                    src={src}
                />}
                <div className={styles.textWrapper43}>{label}</div>
            </div>
        </>
    )
}

const Settings = () => {
    const navigate = useNavigate();
    const [saved, setSaved] = useState(false);
    const [open, setOpen] = useState(false);
    const [user, setUser] = useState(null);
    const [formData, setFormData] = useState({});
    const [options, setOptions] = useState({});
    const [years, setYears] = useState([]);
    const [days, setDays] = useState([]);
    const months = [
        {name: 'Січень', value: 1},
        {name: 'Лютий', value: 2},
        {name: 'Березень', value: 3},
        {name: 'Квітень', value: 4},
        {name: 'Травень', value: 5},
        {name: 'Червень', value: 6},
        {name: 'Липень', value: 7},
        {name: 'Серпень', value: 8},
        {name: 'Вересень', value: 9},
        {name: 'Жовтень', value: 10},
        {name: 'Листопад', value: 11},
        {name: 'Грудень', value: 12},
    ];
    const setLoading = useContext(LoadingContext);

    useEffect(() => {
        apiEndpoint('user/details').fetch()
            .then(res => {
                setUser(res.data);
            }).catch(() => {
            setLoading(false);
            navigate('/sign-in');
        });

        apiEndpoint('user/data').fetch()
            .then(res => setOptions(res.data)).catch(err => console.log(err));

        const yearsMap = [];
        for (let i = 0; i < 100; i++) {
            yearsMap.push(2023 - i);
        }
        setYears(yearsMap);
    }, []);

    useEffect(() => {
        if (formData['month']) {
            const daysMap = [];
            const daysCount = new Date(formData['year'] || 2023, +formData['month'], 0).getDate();
            for (let i = 1; i <= daysCount; i++) {
                daysMap.push(i);
            }
            setDays(daysMap);
        } else {
            const daysMap = [];
            for (let i = 1; i <= 31; i++) {
                daysMap.push(i);
            }
            setDays(daysMap);
        }
    }, [formData['month'], formData['year']]);

    const submit = () => {
        apiEndpoint('user/details').post(formData)
            .then((res) => {
                setUser(res.data);
                setSaved(true);
            })
            .catch(err => console.log(err));
    }

    const addFile = (file) => {
        if (!file)
            return;

        const data = new FormData();
        data.append('picture', file);

        apiEndpoint('user/picture', true).post(data)
            .then((res) => {
                const img = document.getElementById('photo');
                img.src = `https://drive.google.com/uc?export=view&id=${res.data}`;
                img.style.width = '241px';
                img.style.height = '242px';
                launchInfo('Фото успішно змінено');
            }).catch(err => console.log(err));
    }

    setLoading(true);
    if (!user)
        return <></>;
    setLoading(false);

    return (
        <>
            <Header/>
            <LogoutModal open={open} setOpen={setOpen}/>
            <div className={styles.studentPage}>
                <div className={styles.textWrapper12}>Змінити мій профіль</div>
                <div className={styles.divider}>
                    <div className={styles.frame15}>
                        <NavMenu ionIcon={<ion-icon name="file-tray-full" class={styles.img3}></ion-icon>}
                                 label={'Курси'} onClick={() => navigate('/my-courses')}/>
                        <div className={styles.split}></div>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/fluent-person-32-filled.svg'}
                                 label={'Профіль'}/>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/mingcute-chat-1-fill.svg'}
                                 label={'Чати'}/>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/group@2x.png'}
                                 label={'Прогрес'}/>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/mingcute-certificate-2-fill-1.svg'}
                                 label={'Сертифікати'}/>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/mingcute-certificate-2-fill.svg'}
                                 label={'Покупки'}/>
                        <div className={styles.split}></div>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/solar-phone-bold.svg'}
                                 label={'Технічна підтримка'}/>
                        <div className={styles.split}></div>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/majesticons-settings-cog.svg'}
                                 label={'Налаштування'} selected={true}/>
                        <NavMenu src={'https://c.animaapp.com/0N8HhTem/img/pepicons-pop-leave-circle-filled.svg'}
                                 label={'Вийти'} onClick={() => setOpen(true)}/>
                    </div>
                    <div className={styles.groupWrapper}>
                        <div className={styles.group}>
                            <div>
                                <div className={styles.goldTitle}>Основні дані</div>
                                <p className={styles.subTitle}>Розкажіть іншим користувачам OnCourse про себе</p>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Повне ім’я</div>
                                <Input label={"Ім’я Призвище"} value={user.fullName} width={800}
                                       setValue={e => setFormData({...formData, fullName: e})}/>
                            </div>
                            <div className={styles.space}>
                                <div className={styles.title}>Фото профілю</div>
                                <div className={styles.image}>
                                    <div className={styles.imageWrap}>
                                        {
                                            user['photoId'] ?
                                                <img className={styles.photo} alt="Vector" width={240} height={240}
                                                     id={'photo'}
                                                     src={`
                    https://drive.google.com/uc?export=view&id=${user['photoId']}`}/>
                                                :
                                                <img className={styles.photo} alt="Vector" id={'photo'}
                                                     src={"https://c.animaapp.com/0N8HhTem/img/vector-4.svg"}/>
                                        }
                                    </div>
                                    <div className={styles.buttons}>
                                        <div className={styles.buttonWrapper}>
                                            <div className={styles.loadText}>Завантажити фото</div>
                                            <input type="file" className={styles.loadButton}
                                                   onChange={e => addFile(e.target.files[0])}/>
                                        </div>
                                        <p className={styles.littleTitle}>Максимальний розмір 1 МБ. JPG, GIF або PNG</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className={styles.group} style={{gap: 65}}>
                            <div>
                                <div className={styles.goldTitle}>Кар’єрні цілі</div>
                                <p className={styles.subTitle}>
                                    Розкажіть, які нові можливості для своєї кар’єри ви шукаєте
                                </p>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Ви готові до змін та нових можливостей?</div>
                                <div className={styles.radioWrap}>
                                    <Radio options={[{label: "Так", value: true}, {label: "Ні", value: false}]}
                                           value={user['newOpportunities']}
                                           setValue={e => {
                                               setFormData({...formData, newOpportunities: e});
                                               setUser({...user, newOpportunities: e});
                                           }}/>
                                </div>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Хочете отримати диплом?</div>
                                <div className={styles.radioWrap}>
                                    <Radio options={[{label: "Так", value: true}, {label: "Ні", value: false}]}
                                           value={user['getDiploma']}
                                           setValue={e => {
                                               setFormData({...formData, getDiploma: e});
                                               setUser({...user, getDiploma: e});
                                           }}/>
                                </div>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Необхідні навички</div>
                                <Select label={"Розкажіть, які навички ви б хотіли отримувати"} width={800}
                                        options={options['Skills']}
                                        value={user['skills']}
                                        setValue={e => {
                                            setFormData({...formData, skills: e});
                                            setUser({...user, skills: e});
                                        }}/>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Мета навчання</div>
                                <Select label={"Оберіть мету навчання"} width={800}
                                        options={options['Aim']}
                                        value={user['aim']}
                                        setValue={e => {
                                            setFormData({...formData, aim: e});
                                            setUser({...user, aim: e});
                                        }}/>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Професія</div>
                                <Select label={"Вкажіть, яка галузь вас цікавить"} width={800}
                                        options={options['Occupation']}
                                        value={user['occupation']}
                                        setValue={e => {
                                            setFormData({...formData, occupation: e});
                                            setUser({...user, occupation: e});
                                        }}/>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Бажаний рівень</div>
                                <Select label={"Виберіть рівень досвіду"} width={800}
                                        options={options['WantedLevel']}
                                        value={user['wantedLevel']}
                                        setValue={e => {
                                            setFormData({...formData, wantedLevel: e});
                                            setUser({...user, wantedLevel: e});
                                        }}/>
                            </div>
                        </div>
                        <div className={styles.group} style={{gap: 60}}>
                            <div>
                                <div className={styles.goldTitle}>Відомості про вас</div>
                                <p className={styles.subTitle}>
                                    Представтеся спільноті OnCourse. Підтримуйте й налагоджуйте зв’язки з іншими.
                                </p>
                            </div>
                            <div className={styles.space}>
                                <div className={styles.title}>Про мене</div>
                                <Multiline label={"Розкажіть нам про себе. Ваші захоплення, цілі, досвід, тощо"}
                                           width={800} value={user['about']}
                                           setValue={e => setFormData({...formData, about: e})}/>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Місце</div>
                                <Input label={"Вкажіть місто, область чи країну, в якій зараз мешкаєте"}
                                       width={800} value={user['location']}
                                       setValue={e => setFormData({...formData, location: e})}/>
                            </div>
                            <div className={styles.space}>
                                <div className={styles.title}>Стать</div>
                                <div className={styles.splitRadio} style={{width: '800px'}}>
                                    <Radio options={[
                                        {label: "Жінка", value: 'Жінка'},
                                        {label: "Чоловік", value: 'Чоловік'},
                                        {label: "Інше", value: 'Інше'}]}
                                           value={user['sex']}
                                           setValue={e => {
                                               setFormData({...formData, sex: e});
                                               setUser({...user, sex: e});
                                           }}/>
                                </div>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>День народження</div>
                                <div className={styles.inputGroup} style={{width: '800px'}}>
                                    <Select label={"День"} options={days} value={user['day']}
                                            setValue={e => {
                                                setFormData({...formData, day: e});
                                                setUser({...user, day: +e});
                                            }}/>
                                    <Select label={"Місяць"} options={months} value={user['month']}
                                            setValue={e => {
                                                setFormData({...formData, month: e});
                                                setUser({...user, month: +e});
                                            }}/>
                                    <Select label={"Рік"} options={years} value={user['year']}
                                            setValue={e => {
                                                setFormData({...formData, year: e});
                                                setUser({...user, year: +e});
                                            }}/>
                                </div>
                            </div>
                            <div className={styles.spaceCenter}>
                                <div className={styles.title}>Номер телефону</div>
                                <div className={styles.inputGroup} style={{width: '800px'}}>
                                    <Select label={"UA"} value={" "} options={[]} gap={20}/>
                                    <Input label={"Номер телефону"} value={user['phoneNumber']}
                                           setValue={e => setFormData({...formData, phoneNumber: e})}/>
                                </div>
                            </div>
                            <div className={styles.space}>
                                <div className={styles.title} style={{width: 250}}>Конфідунційність профілю</div>
                                <div className={styles.inputGroup}
                                     style={{width: '800px', flexDirection: 'column', gap: 30}}>
                                    <Select label={"Лише Я"} value={" "} options={[]} gap={35}/>
                                    <p>
                                        <span className={styles.span}>
                                            Примітка. У дописах на дискусійних форумах і в рецензованих роботах інші учні ваших курсів завжди
                                            бачитимуть ваше ім’я та зображення профілю. В оцінках курсу та в надісланих вами рецензіях зображення
                                            вашого профілю буде видно всім, хто переглядає каталог OnCourse. Додаткові відомості див. у нашому
                                            документі{" "}
                                        </span>
                                        <a href="https://www.coursera.org/about/privacy" rel="noopener noreferrer"
                                           target="_blank" className={styles.link}>
                                            <span className="text-wrapper-28">Політика конфіденційності</span>
                                        </a>
                                        <span className={styles.span}>.</span>
                                    </p>
                                </div>
                            </div>
                            <div className={styles.outroWrap}>
                                <div className={styles.save} onClick={submit}>
                                    <div className={styles.button}>Зберегти зміни</div>
                                </div>
                                {saved && <div className={styles.saved}>Ваші зміни успішно збережено</div>}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Footer/>
        </>
    )
}

export default Settings;