import styles from './sign-up.module.css';
import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {apiEndpoint} from "../../api";
import {launchInfo} from "../../components/layout/Layout";

const SighUp = () => {
    const [state, setState] = useState(0);
    const navigate = useNavigate();

    return (
        <div className={styles.mainPage}>
            <div className={styles.mainPageV}>
                <div className={styles.overlap}>
                    <div className={styles.blur}>
                        <div className={styles.right}>
                            <img
                                className={styles.pajamasGoBack}
                                alt="Pajamas go back"
                                src="https://c.animaapp.com/5ThCsLyF/img/pajamas-go-back.svg"
                                onClick={() => navigate('/')}
                            />
                            <div className={styles.group5}>
                                <img className={styles.image} alt="logo"
                                     src="https://c.animaapp.com/5ThCsLyF/img/----------------.svg"/>
                                <div className={styles.divWrapper}>
                                    <p className={styles.textWrapper11}>
                                        Опануйте актуальні навички на онлайн-курсах від кращих викладачів на
                                        найбільшій освітній платформі України!
                                    </p>
                                </div>
                            </div>
                            <div className={styles.overlapGroupWrapper}>
                                <div className={styles.overlapGroup2}>
                                    <div className={styles.rectangle3}>
                                        <p className={styles.div4}>
                                            <span className={styles.textWrapper10}>Виникли проблеми?<br/></span>
                                            <span className={styles.textWrapper7}>Служба підтримки</span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className={styles.left}>
                            <FirstStep show={state === 0} setState={setState}/>
                            <LoginForm show={state === 1} setState={setState}/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

const FirstStep = ({show, setState}) => {
    return (
        <div className={`${styles.firstOverlapGroup} ${!show ? styles.hidden : ""}`}>
            <div className={styles.padding}>
                <div className={styles.firstDiv}>Зареєструватися за допомогою Google</div>
                <p className={styles.firstP}>Спрощена система реєстрації, зручний вхід в подальшому.</p>
                <div className={styles.firstComponent}>
                    <div className={styles.google}>
                        <img
                            className={styles.firstFlatColorIcons}
                            alt="Flat color icons"
                            src={"https://c.animaapp.com/x0avctM1/img/flat-color-icons-google-3.png"}
                        />
                        <div className={styles.firstTextWrapper}>Зареєструватися з Google</div>
                    </div>
                </div>
            </div>
            <div className={styles.firstGroup2}>
                <div className={styles.firstLine}/>
                <div className={styles.or}>Або</div>
                <div className={styles.firstLine}/>
            </div>
            <div className={styles.padding}>
                <div className={styles.firstTextWrapper2}>Зареєструватися за допомогою пошти</div>
                <p className={styles.firstTextWrapper3}>
                    Увімкніть автоматичне збереження вашого логіну та паролю щоб спростити вхід на платформу.
                </p>
                <div className={styles.firstComponent}>
                    <div className={styles.login} onClick={() => setState(1)}>
                        Зареєструватись зараз
                    </div>
                </div>
            </div>
        </div>
    )
}

const LoginForm = ({show}) => {
    const [role, setRole] = useState(null);
    const [checked, setChecked] = useState(false);
    const navigate = useNavigate();
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phone, setPhone] = useState('');

    const signUp = (e) => {
        e.preventDefault();

        if (role === null) {
            launchInfo('Оберіть роль');
            return;
        }

        if (!checked) {
            launchInfo('Погодьтеся з умовами');
            return;
        }

        apiEndpoint('auth/sign-up')
            .post({email, password, firstName, lastName, phoneNumber: phone, teacher: role === 1})
            .then(res => {
                localStorage.setItem('token', res.data.token);
                navigate('/');
            })
            .catch(err => console.log(err));
    }

    return (
        <div className={`${styles.rectangle2} ${!show ? styles.hidden : ""}`}>
            <div style={{marginBottom: "20px"}}>
                <div className={styles.textWrapper9}>Реєстрація облікового запису</div>
                <p className={styles.p}>Зареєструйся і почни проходити курси вже зараз!</p>
            </div>
            <div>
                <form className={styles.group2} onSubmit={signUp}>
                    <div className={styles.frame}>
                        <img className={styles.img2} alt="Gg profile"
                             src="https://c.animaapp.com/5ThCsLyF/img/gg-profile.svg"/>
                        <input placeholder={'Ім’я'} className={styles.input} value={firstName} required
                               onChange={(e) => setFirstName(e.target.value)}/>
                    </div>
                    <div className={styles.frame}>
                        <img
                            className={styles.img2}
                            alt="Ri profile line"
                            src="https://c.animaapp.com/5ThCsLyF/img/ri-profile-line.svg"
                        />
                        <input placeholder={'Призвіще'} className={styles.input} value={lastName} required
                               onChange={(e) => setLastName(e.target.value)}/>
                    </div>
                    <div className={styles.frame}>
                        <img
                            className={styles.img2}
                            alt="Tabler phone call"
                            src="https://c.animaapp.com/5ThCsLyF/img/tabler-phone-call.svg"
                        />
                        <input placeholder={'Номер телефону'} className={styles.input} type={'phone'} value={phone}
                               required
                               onChange={(e) => setPhone(e.target.value)}/>
                    </div>
                    <div className={styles.frame}>
                        <img className={styles.img2} alt="My email"
                             src="https://c.animaapp.com/5ThCsLyF/img/mi-email.svg"/>
                        <input placeholder={'Електронна пошта'} className={styles.input} type={'email'} value={email}
                               required
                               onChange={(e) => setEmail(e.target.value)}/>
                    </div>
                    <div className={styles.frame}>
                        <img
                            className={styles.img2}
                            alt="Mdi password outline"
                            src="https://c.animaapp.com/5ThCsLyF/img/mdi-password-outline.svg"
                        />
                        <input placeholder={'Пароль'} className={styles.input} type={'password'} value={password}
                               required
                               onChange={(e) => setPassword(e.target.value)}/>
                    </div>
                    <div className={styles.group4}>
                        <div className={styles.component}>
                            <div className={styles.div} onClick={() => setRole(0)}>
                                {role === 0 ?
                                    <div className={styles.overlapGroup}>
                                        <img className={styles.vector} alt="Vector"
                                             src="https://c.animaapp.com/5ThCsLyF/img/vector-5.svg"/>
                                    </div> : <div className={styles.unchecked}/>
                                }
                                <div className={styles.textWrapper}>Я - Студент</div>
                            </div>
                            <div className={styles.div} onClick={() => setRole(1)}>
                                {role === 1 ?
                                    <div className={styles.overlapGroup}>
                                        <img className={styles.vector} alt="Vector"
                                             src="https://c.animaapp.com/5ThCsLyF/img/vector-5.svg"/>
                                    </div> : <div className={styles.unchecked}/>
                                }
                                <div className={styles.textWrapper}>Я - Викладач</div>
                            </div>
                        </div>
                    </div>
                    <div className={styles.group3}>
                        <div className={styles.group} onClick={() => setChecked(!checked)}>
                            {checked && <img className={styles.uilCheck} alt="Uil check"
                                             src="https://c.animaapp.com/5ThCsLyF/img/uil-check.svg"/>}
                        </div>
                        <p className={styles.div3}>
                            <span className={styles.span}>Погоджуюся з </span>
                            <span className={styles.textWrapper7}>положеннями і умовами</span>
                            <span className={styles.span}> та </span>
                            <span className={styles.textWrapper7}>політикою конфіденційності</span>
                            <span className={styles.span}>.</span>
                        </p>
                    </div>
                    <div className={styles.propertyFrameWrapper}>
                        <button type={'submit'} className={styles.textWrapper3}>
                            Зареєструватися
                        </button>
                    </div>
                    <p className={styles.div2}>
                        <span className={styles.span}>Уже маєте обліковий запис? </span>
                        <span className={styles.textWrapper7} onClick={() => navigate('/sign-in')}>Увійти</span>
                    </p>
                </form>
            </div>
        </div>
    )
}

export default SighUp;