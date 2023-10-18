import styles from './payment.module.css';
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import {useEffect, useState} from "react";
import {SuccessPayment} from "../../components/modal-windows/ModalWindows";
import {useNavigate, useParams} from "react-router-dom";
import {apiEndpoint} from "../../api";
import {launchError, launchInfo} from "../../components/layout/Layout";

const Payment = () => {
    const [active, setActive] = useState(false);
    const [open, setOpen] = useState(false);
    const {courseId} = useParams();
    const [course, setCourse] = useState(null);
    const navigate = useNavigate();
    const [email, setEmail] = useState('');

    useEffect(() => {
        apiEndpoint('check/' + courseId).fetch()
            .then(res => {
                if (res.data === false)
                    navigate('/course/' + courseId);
                else
                    setCourse(res.data)
            }).catch(() => navigate('/course/' + courseId));
    }, [courseId, navigate]);

    const pay = () => {
        const payment = {cardNumber: '', expiryDate: '', cvv: '', email: email};

        const elements = document.querySelectorAll('#otp input');
        for (let element of elements) {
            if (element.value === '') {
                element.focus();
                launchInfo('Заповніть всі поля');
                return;
            }

            payment[element.name] += element.value.toString();
        }

        apiEndpoint('pay/' + courseId).post(payment)
            .then(res => setOpen(res.data))
            .catch(err => launchError(err));
    }

    if (!course) return (<></>);

    return (
        <>
            <Header/>
            <SuccessPayment open={open} setOpen={setOpen}/>
            <div className={styles.paymentPage}>
                <div className={styles.paymentPageV}>
                    <div className={styles.group7}>
                        <div className={styles.textWrapper13}>Оплата курсу</div>
                    </div>
                    <div className={styles.main}>
                        <div className={styles.overlapGroup}>
                            <div className={styles.textWrapper14}>Замовлення №321</div>
                            <p className={styles.textWrapper20}>{course.name}</p>
                            <img className={styles.vector2} alt="Vector"
                                 src="https://c.animaapp.com/ppLLXIZ3/img/vector-78.svg"/>
                            <div className={styles.group8}>
                                <div className={styles.textWrapper16}>До сплати</div>
                                <p className={styles.element}>
                                    <span className={styles.span}>{course['price']} </span>
                                    <span className={styles.textWrapper15}>грн</span>
                                </p>
                            </div>
                            <div className={styles.frame4} onClick={() => setActive(false)}>
                                <img
                                    className={styles.img3}
                                    alt="Flat color icons"
                                    src="https://c.animaapp.com/ppLLXIZ3/img/flat-color-icons-google.svg"
                                />
                                <div className={styles.textWrapper18}>Pay</div>
                            </div>
                            <p className={styles.textWrapper17}>Або скористайтесь іншим способом оплати</p>
                            <div className={`${styles.frame5} ${active ? styles.active : ''}`}
                                 onClick={() => setActive(true)}>
                                <ion-icon class={styles.icon} name={"card"}></ion-icon>
                                <div className={styles.textWrapper19}>Платіжна картка</div>
                            </div>
                        </div>
                        <img className={styles.vector3} alt="Vector"
                             src="https://c.animaapp.com/ppLLXIZ3/img/vector-79.svg"/>
                        <div className={`${styles.card} ${active ? '' : styles.hidden}`}>
                            <div className={styles.group11}>
                                <img className={styles.group12} alt="Group"
                                     src="https://c.animaapp.com/ppLLXIZ3/img/group-375@2x.png"/>
                                <div className={styles.group13}>
                                    <div className={styles.group14}>
                                        <div className={styles.textWrapper24}>НОМЕР КАРТКИ</div>
                                        <img className={styles.vector4} alt="Vector"
                                             src="https://c.animaapp.com/ppLLXIZ3/img/vector-80.svg"/>
                                    </div>
                                    <Card length={16} gap={4} name={'cardNumber'}/>
                                </div>
                                <div className={styles.union}>
                                    <div className={styles.group13}>
                                        <div className={styles.textWrapper24}>ММ/РР</div>
                                        <Card length={5} split={2} name={'expiryDate'}/>
                                    </div>
                                    <div className={styles.group13}>
                                        <div className={styles.textWrapper24}>CVV/CVC</div>
                                        <Card length={3} name={'cvv'}/>
                                    </div>
                                </div>
                            </div>
                            <div className={styles.group10}>
                                <label className={styles.textWrapper23}>Електронна пошта</label>
                                <div className={styles.divWrapper}>
                                    <input className={styles.textWrapper22}
                                           onChange={(e) =>
                                               setEmail(e.target.value)}/>
                                </div>
                            </div>
                            <div className={styles.component} onClick={pay}>
                                <div className={styles.textWrapper10}>Сплатити</div>
                            </div>
                        </div>
                    </div>
                    <div className={styles.group9}>
                        <img
                            className={styles.iconParkSolid}
                            alt="Icon park solid"
                            src="https://c.animaapp.com/ppLLXIZ3/img/icon-park-solid-protect.svg"
                        />
                        <div className={styles.textWrapper21}>Дані покупця захищені</div>
                    </div>
                </div>
            </div>
            <Footer/>
        </>
    )
}

const Card = ({length, gap, split, name}) => {
    const OTPInput = () => {
        const editor = document.getElementById('first');
        editor.onpaste = pasteOTP;

        const inputs = document.querySelectorAll('#otp input');
        for (let i = 0; i < inputs.length; i++) {
            inputs[i].addEventListener('keydown', (event) => {
                event.preventDefault();
                if (event.key === "Backspace") {
                    inputs[i].value = '';
                    if (i !== 0)
                        inputs[i - 1].focus();
                } else {
                    if (i === inputs.length - 1 && inputs[i].value !== '') {
                        return true;
                    } else if (event.keyCode > 47 && event.keyCode < 58) {
                        inputs[i].value = event.key;
                        if (i !== inputs.length - 1)
                            inputs[i + 1].focus();
                    }
                }
            });
        }
    }

    const pasteOTP = (event) => {
        event.preventDefault();
        let elm = event.target;
        let pasteVal = event.clipboardData.getData('text').split("");
        if (pasteVal.length > 0) {
            while (elm) {
                elm.value = pasteVal.shift();
                elm = elm.nextSibling.nextSibling;
            }
        }
    }

    useEffect(() => {
        OTPInput();
        // eslint-disable-next-line
    }, []);

    const mapLength = (length) => {
        let arr = [];
        for (let i = 0; i < length; i++) {
            arr.push(i);
        }
        return arr;
    }

    return (
        <>
            <div className={styles.wrap}>
                <div id="otp" className={styles.otp}>
                    {
                        mapLength(length).map((item, index) =>
                            <div key={'Input' + index}>
                                {
                                    split && index === split ?
                                        <div className={styles.formControl}>/</div> :
                                        <>
                                            <input
                                                placeholder={" "} name={name}
                                                className={`${styles.formControl} ${gap && (index + 1) % gap === 0 ? styles.gap : ''}`}
                                                type="text" id={index === 0 ? "first" : ''} maxLength="1"/>
                                            <div className={styles.pseudo}></div>
                                        </>
                                }
                            </div>
                        )
                    }
                </div>
            </div>
        </>
    )
}

export default Payment;