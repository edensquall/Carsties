import EmptyFilter from '@/app/components/EmptyFilter'
import React, { use } from 'react'

export default function SignIn({ searchParams }: { searchParams: Promise<{ callbackUrl: string }> }) {
  const { callbackUrl } = use(searchParams);

  return (
    <EmptyFilter
      title='You need to be logged in to do that'
      subtitle='Please click below to login'
      showLogin
      callbackUrl={callbackUrl}
    />
  )
}
