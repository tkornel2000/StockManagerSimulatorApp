import { Navbar } from './Navbar'
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const MyProfile = () => {
  PermissionForComponent();

  return (
    <div>
        <Navbar/>
    </div>
  )
}
