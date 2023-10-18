import {useNavigate, useSearchParams} from "react-router-dom";
import styles from "./reset-password.module.css";
import {useEffect, useState} from "react";
import {apiEndpoint} from "../../api";
import {launchError, launchInfo} from "../../components/layout/Layout";

const ResetPassword = () => {
    const navigate = useNavigate();
    const [params,] = useSearchParams();

    const token = params.get('token');
    const email = params.get('email');

    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    useEffect(() => {
        if (!token || !email) {
            navigate('/');
        }
    }, []);

    const resetPassword = () => {
        if (confirmPassword !== password) {
            launchInfo('Паролі не співпадають');
            return;
        }

        apiEndpoint('auth/reset-password').post({email, password, token})
            .then(() => navigate('/sign-in')).catch(err => launchError(err));
    }

    return (
        <>
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
                                    <div>
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
                                <div className={styles.firstOverlapGroup}>
                                    <div className={styles.wrap}>
                                        <div className={styles.firstDiv}>
                                            Новий пароль
                                        </div>
                                        <p className={styles.firstP}>Введіть новий пароль для обліковго запису.</p>
                                        <form className={styles.group2}>
                                            <div className={styles.frame}>
                                                <img
                                                    className={styles.img2}
                                                    alt="Password"
                                                    src="https://c.animaapp.com/5ThCsLyF/img/mdi-password-outline.svg"
                                                />
                                                <input placeholder={'Пароль'} className={styles.input}
                                                       type={'password'} autoComplete={'new-password'} value={password}
                                                       onChange={e => setPassword(e.target.value)}/>
                                            </div>
                                            <div className={styles.frame}>
                                                <img
                                                    className={styles.img2}
                                                    alt="Password"
                                                    src="https://c.animaapp.com/5ThCsLyF/img/mdi-password-outline.svg"
                                                />
                                                <input placeholder={'Підтвердіть пароль'} className={styles.input}
                                                       type={'password'} value={confirmPassword}
                                                       autoComplete={'new-password'}
                                                       onChange={e => setConfirmPassword(e.target.value)}/>
                                            </div>
                                            <div className={styles.propertyFrameWrapper} onClick={resetPassword}>
                                                <div className={styles.textWrapper3}>
                                                    Змінити пароль
                                                </div>
                                            </div>
                                            <p className={styles.div2}>
                                                <span className={styles.textWrapper7}
                                                      onClick={() => navigate('/sign-in')}>Увійти в аккаунт ?</span>
                                            </p>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ResetPassword;