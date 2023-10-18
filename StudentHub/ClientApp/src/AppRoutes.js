import SignUp from "./pages/sign-up/SignUp";
import SignIn from "./pages/sign-in/SignIn";
import ResetPassword from "./pages/reset-password/ResetPassword";
import Home from "./pages/home/Home";
import CoursePage from "./pages/course-page/CoursePage";
import CourseCatalogue from "./pages/course-catalogue/CourseCatalogue";
import MyCourses from "./pages/my-courses/MyCourses";
import Settings from "./pages/settings/Settings";
import ViewCourse from "./pages/view-course/ViewCourse";
import Payment from "./pages/payment/Payment";

const AppRoutes = [
    {
        index: true,
        element: <Home/>,
    },
    {
        path: '/sign-up',
        element: <SignUp/>
    },
    {
        path: '/sign-in',
        element: <SignIn/>
    },
    {
        path: '/reset-password',
        element: <ResetPassword/>
    },
    {
        path: "/course/:id",
        element: <CoursePage/>
    },
    {
        path: '/catalogue',
        element: <CourseCatalogue/>
    },
    {
        path: '/my-courses',
        element: <MyCourses/>
    },
    {
        path: '/settings',
        element: <Settings/>
    },
    {
        path: '/view-course/:id',
        element: <ViewCourse/>
    },
    {
        path: '/pay/:courseId',
        element: <Payment/>
    }
];

export default AppRoutes;
