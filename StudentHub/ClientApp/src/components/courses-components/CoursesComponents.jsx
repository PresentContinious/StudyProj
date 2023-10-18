import styles from './courses-components.module.css';
import {getStars} from "../../pages/course-page/CoursePage";
import {useState} from "react";

export const Reviews = ({course}) => {
    const data = [
        {name: 'Денис Васильчук', src: 'https://c.animaapp.com/BU0gcmOz/img/ellipse-6@2x.png'},
        {name: 'Олександр Микитюк', src: 'https://c.animaapp.com/GuRgT161/img/ellipse-5-8@2x.png'},
        {name: 'Євгенія Авраменко', src: 'https://c.animaapp.com/BU0gcmOz/img/ellipse-7@2x.png'},
        {name: 'Павло Скоропатський', src: 'https://c.animaapp.com/BU0gcmOz/img/ellipse-7@2x.png'}];
    const [current, setCurrent] = useState(0);

    const getLeft = () => {
        if (current === 0)
            return data[data.length - 1];

        return data[current - 1];
    }

    const getRight = () => {
        if (current === data.length - 1)
            return data[0];

        return data[current + 1];
    }

    return (
        <div className={styles.overlap6}>
            <div className={styles.centerTitle}>
                <div className={styles.textWrapper40}>Відгуки про курс</div>
                <p className={styles.textWrapper41}>
                    Дізнайтеся що думають користувачі які вже пройшли цей курс
                </p>
            </div>
            <div className={styles.propertyGroupWrapper}>
                {getLeft() && <div className={styles.groupWrapper} style={{left: '-9%'}}>
                    <div className={styles.group33} style={{gap: 20}}>
                        <img
                            className={styles.ellipse3}
                            alt="Ellipse"
                            src={getLeft().src}
                        />
                        <div className={styles.group34}>
                            <div className={styles.div3}>{getLeft().name}</div>
                            <div className={styles.textWrapper20}>Lorem Ipsum</div>
                            <div className={styles.group35}>
                                <div className={styles.textWrapper21}>4</div>
                                {getStars(4)}
                            </div>
                        </div>
                    </div>
                    <p className={styles.textWrapper19}>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi eget urna
                        et turpis porta vestibulum. Aenean sollicitudin mauris a sapien semper,
                        sed auctor arcu finibus. Aliquam condimentum eros et ligula euismod
                        interdum vitae vel justo. Sed consectetur enim eget felis mattis convallis.
                        Nullam in lectus eu nulla ultrices sollicitudin. Maecenas vitae libero dui
                        Integer.
                    </p>
                </div>}
                <div className={styles.main}>
                    <img
                        className={styles.icRoundNavigate5}
                        alt="Ic round navigate"
                        onClick={() => setCurrent(current > 0 ? current - 1 : data.length - 1)}
                        src="https://c.animaapp.com/GuRgT161/img/ic-round-navigate-next-1.svg"
                    />
                    <div className={styles.group32}>
                        <div className={styles.group33}>
                            <img
                                className={styles.ellipse2}
                                alt="Ellipse"
                                src={data[current].src}
                            />
                            <div className={styles.group34}>
                                <div className={styles.textWrapper23}>{data[current].name}</div>
                                <div className={styles.textWrapper24}>Lorem Ipsum</div>
                                <div className={styles.group35}>
                                    <div className={styles.textWrapper25}>4</div>
                                    {getStars(4)}
                                </div>
                            </div>
                        </div>
                        <p className={styles.textWrapper22}>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi eget urna
                            et turpis porta vestibulum. Aenean sollicitudin mauris a sapien semper,
                            sed auctor arcu finibus. Aliquam condimentum eros et ligula euismod
                            interdum vitae vel justo. Sed consectetur enim eget felis mattis convallis.
                            Nullam in lectus eu nulla ultrices sollicitudin. Maecenas vitae libero dui.
                            Integer non dolor eu arcu consequat. Lorem ipsum dolor sit amet, consectetur
                            adipiscing elit. Morbi eget urna et turpis porta vestibulum. Aenean
                            sollicitudin mauris a sapien semper, sed.
                        </p>
                    </div>
                    <img
                        onClick={() => setCurrent(current < data.length - 1 ? current + 1 : 0)}
                        className={styles.icRoundNavigate5}
                        alt="Ic round navigate"
                        src="https://c.animaapp.com/GuRgT161/img/ic-round-navigate-next.svg"
                    />
                </div>
                {getRight() && <div className={styles.groupWrapper} style={{right: '-9%'}}>
                    <div className={styles.group33} style={{gap: 20}}>
                        <img
                            className={styles.ellipse3}
                            alt="Ellipse"
                            src={getRight().src}
                        />
                        <div className={styles.group34}>
                            <div className={styles.div3}>{getRight().name}</div>
                            <div className={styles.textWrapper20}>Lorem Ipsum</div>
                            <div className={styles.group35}>
                                <div className={styles.textWrapper21}>4</div>
                                {getStars(4)}
                            </div>
                        </div>
                    </div>
                    <p className={styles.textWrapper19}>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi eget urna
                        et turpis porta vestibulum. Aenean sollicitudin mauris a sapien semper,
                        sed auctor arcu finibus. Aliquam condimentum eros et ligula euismod
                        interdum vitae vel justo. Sed consectetur enim eget felis mattis convallis.
                        Nullam in lectus eu nulla ultrices sollicitudin. Maecenas vitae libero dui
                        Integer.
                    </p>
                </div>}
            </div>
        </div>
    )
}

export const GonnaLearn = ({sessions}) => {
    return (
        <div className={styles.group40}>
            <div className={styles.group41}>
                <div className={styles.textWrapper30}>Що ви вивчите?</div>
                <div className={styles.group41} style={{gap: 50}}>
                    {
                        sessions.map((item, index) => (
                            <div className={styles.group42} key={index}>
                                <img
                                    className={styles.uilCheck2}
                                    alt="Uil check"
                                    src="https://c.animaapp.com/GuRgT161/img/uil-check-7.svg"
                                />
                                <p className={styles.textWrapper31}>{item.name}</p>
                            </div>
                        ))}
                </div>
            </div>
            <div className={styles.overlapGroupWrapper}>
                <div className={styles.overlapGroup3}>
                    <div className={styles.textWrapper33}>Цей курс включає:</div>
                    <div className={styles.group50}>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Ph video fill"
                                src="https://c.animaapp.com/GuRgT161/img/ph-video-fill.svg"
                            />
                            <div className={styles.textWrapper34}>19,5 годин відеоматеріалів</div>
                        </div>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Fluent task list"
                                src="https://c.animaapp.com/GuRgT161/img/fluent-task-list-square-ltr-24-filled.svg"
                            />
                            <div className={styles.textWrapper34}>20 практичних завдань</div>
                        </div>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Fluent document"
                                src="https://c.animaapp.com/GuRgT161/img/fluent-document-32-filled.svg"
                            />
                            <div className={styles.textWrapper34}>15 теоретичних статей</div>
                        </div>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Mingcute folder"
                                src="https://c.animaapp.com/GuRgT161/img/mingcute-folder-download-fill.svg"
                            />
                            <div className={styles.textWrapper34}>61 ресурс для завантаження</div>
                        </div>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Ph infinity bold"
                                src="https://c.animaapp.com/GuRgT161/img/ph-infinity-bold.svg"
                            />
                            <div className={styles.textWrapper34}>Повний довічний доступ</div>
                        </div>
                        <div className={styles.group51}>
                            <img
                                className={styles.img4}
                                alt="Fluent certificate"
                                src="https://c.animaapp.com/GuRgT161/img/fluent-certificate-16-filled.svg"
                            />
                            <div className={styles.textWrapper35}>Сертифікат при закінченні</div>
                        </div>
                    </div>
                    <div className={styles.component}>
                        <div className={styles.textWrapper}>Продовжити навчання</div>
                    </div>
                </div>
            </div>
        </div>
    )
}