import { Navbar } from './Navbar';
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const Dashboard = () => {
  PermissionForComponent()

  return (
    <Navbar/>
  )
}