import styles from './sign-in.module.css';
import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {apiEndpoint} from "../../api";
import {launchError, launchSuccess} from "../../components/layout/Layout";

const SignIn = () => {
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
                            <SignInComponent show={state === 0} setState={setState}/>
                            <ForgotPassword show={state === 1} setState={setState}/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

const SignInComponent = ({show, setState}) => {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const signIn = (e) => {
        e.preventDefault();

        apiEndpoint('auth/sign-in')
            .post({email, password})
            .then(res => {
                localStorage.setItem('token', res.data.token);
                navigate('/');
            })
            .catch(err => launchError(err));
    }

    return (
        <div className={`${styles.firstOverlapGroup} ${!show ? styles.hidden : ""}`}>
            <div>
                <div className={styles.firstDiv}>Вхід до облікого запису</div>
                <p className={styles.firstP}>Введіть ваші дані, щоб увійти до облікового запису.</p>
                <form className={styles.group2} onSubmit={signIn}>
                    <div className={styles.frame}>
                        <img
                            className={styles.img2}
                            alt="Email"
                            src="https://c.animaapp.com/5ThCsLyF/img/mi-email.svg"
                        />
                        <input placeholder={'Електронна пошта'} className={styles.input} type={'email'}
                               value={email} onChange={e => setEmail(e.target.value)}
                               autoComplete={'email'} required/>
                    </div>
                    <div className={styles.frame}>
                        <img
                            className={styles.img2}
                            alt="Password"
                            src="https://c.animaapp.com/5ThCsLyF/img/mdi-password-outline.svg"
                        />
                        <input placeholder={'Пароль'} className={styles.input} type={'password'}
                               value={password} onChange={e => setPassword(e.target.value)}
                               autoComplete={'current-password'} required/>
                    </div>
                    <div className={styles.propertyFrameWrapper}>
                        <button type={'submit'} className={styles.textWrapper3}>
                            Увійти
                        </button>
                    </div>
                    <p className={styles.div2}>
                        <span className={styles.textWrapper7} onClick={() => setState(1)}>Забули пароль ?</span>
                    </p>
                </form>
            </div>
            <div className={styles.firstGroup2}>
                <img className={styles.firstLine} alt="Line" src="https://c.animaapp.com/9fpgOLbO/img/line-22.svg"/>
                <div className={styles.or}>Або</div>
                <img className={styles.firstLine} alt="Line" src="https://c.animaapp.com/9fpgOLbO/img/line-22.svg"/>
            </div>
            <div>
                <div className={styles.firstComponent}>
                    <div className={styles.google}>
                        <img
                            className={styles.firstFlatColorIcons}
                            alt="Flat color icons"
                            src={"https://c.animaapp.com/x0avctM1/img/flat-color-icons-google-3.png"}
                        />
                        <div className={styles.firstTextWrapper}>Увійти за допомогою Google</div>
                    </div>
                </div>
                <p className={styles.div2}>
                    <span className={styles.span}>Немає облікого запису? </span>
                    <span className={styles.textWrapper7} onClick={() => navigate('/sign-up')}>Зареєструватись</span>
                </p>
            </div>
        </div>
    )
}

const ForgotPassword = ({show, setState}) => {
    const [active, setActive] = useState(true);
    const [email, setEmail] = useState('');

    const handleClick = () => {
        apiEndpoint('auth/reset-password-request').post(email)
            .then(res => {
                launchSuccess(res);
                setActive(false);
            })
            .catch(err => launchError(err));
    }

    return (
        <div className={`${styles.firstOverlapGroup} ${!show ? styles.hidden : ""}`} style={{padding: "10% 10%"}}>
            <div className={styles.center}>
                <div className={styles.firstDiv}>Забули пароль?</div>
                <p className={styles.p}>Будь ласка, введіть адресу своєї електронної пошти у форму нижче і ми
                    надішлемо вам інструкції щодо заміни пароля.</p>
                <div className={styles.frame} style={{width: '90%'}}>
                    <img
                        className={styles.img2}
                        alt="Email"
                        src="https://c.animaapp.com/5ThCsLyF/img/mi-email.svg"
                    />
                    <input placeholder={'Електронна пошта'} className={styles.input} type={'email'}
                           value={email} onChange={e => setEmail(e.target.value)}
                           autoComplete={'email'} disabled={!active}/>
                </div>
                <div className={`${styles.propertyFrameWrapper} ${active ? '' : styles.disable}`}
                     onClick={handleClick}>
                    <div className={styles.textWrapper3}>
                        Відновити пароль
                    </div>
                </div>
                <p className={styles.div2}>
                    <span className={styles.span}>Пам'ятаєте свій пароль? </span>
                    <span className={styles.textWrapper7} onClick={() => setState(0)}>Увійти</span>
                </p>
            </div>
        </div>
    )
}

export default SignIn;