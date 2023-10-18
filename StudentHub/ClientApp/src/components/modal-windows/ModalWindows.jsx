import {useEffect} from "react";
import styles from './modal-windows.module.css';
import {useNavigate} from "react-router-dom";

export const SuccessPayment = ({open, setOpen}) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (open) {
            document.body.style.overflow = 'hidden';
            document.body.height = '100%';
            document.body.width = '100%';
        } else {
            document.body.style.overflow = 'unset';
            document.body.height = 'unset';
            document.body.width = 'unset';
        }
    }, [open]);

    const closeModal = (id) => {
        setOpen(false);

        document.body.style.overflow = 'unset';
        document.body.height = 'unset';
        document.body.width = 'unset';

        if (id)
            navigate('/course/' + id);
    }

    return (
        <div className={styles.modal} style={{top: `${open ? '0' : '-100%'}`}} id={'modal'}>
            <div className={styles.frame}>
                <div className={styles.div2}>
                    <div className={styles.overlap}>
                        <p className={styles.p}>{open.name}, дякуємо за те, що користуєтеся OnCourse!</p>
                        <div className={styles.textWrapper2}>Оплату проведено успішно</div>
                    </div>
                    <div className={styles.overlapGroup}>
                        <p className={styles.onCourse}>
                            Дякуємо за вибір OnCourse! Оплачений курс незабаром з'явиться у вашому особистому
                            кабінеті.
                        </p>
                        <div className={styles.component} onClick={() => closeModal(open)}>
                            <div className={styles.div}>
                                Перейти до курсу
                            </div>
                        </div>
                        <div className={styles.propertyDefault} onClick={() => closeModal('/')}>
                            <div className={styles.textWrapper}>
                                Повернутися на головну
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export const LogoutModal = ({open, setOpen}) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (open) {
            document.body.style.overflow = 'hidden';
            document.body.height = '100%';
            document.body.width = '100%';
        } else {
            document.body.style.overflow = 'unset';
            document.body.height = 'unset';
            document.body.width = 'unset';
        }
    }, [open]);

    const closeModal = (path) => {
        setOpen(false);

        document.body.style.overflow = 'unset';
        document.body.height = 'unset';
        document.body.width = 'unset';

        if (path) {
            localStorage.removeItem('token');
            navigate(path);
        }
    }

    return (
        <div className={styles.modal} style={{top: `${open ? '0' : '-100%'}`}} id={'modal'}>
            <div className={styles.logout}>
                <div className={styles.div2}>
                    <div className={styles.overlap}>
                        <p className={styles.p}>Ви впевнені, що хочете вийти з облікового запису?</p>
                        <div className={styles.textWrapper2}>Вихід з облікового запису</div>
                    </div>
                    <div className={styles.overlapGroup}>
                        <div className={styles.stay} onClick={() => closeModal()}>
                            <div className={styles.stayText}>
                                Залишитись
                            </div>
                        </div>
                        <div className={styles.component} onClick={() => closeModal('/sign-in')}>
                            <div className={styles.div}>
                                Так, вийти
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}