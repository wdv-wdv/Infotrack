import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import PageHeader from '../PageHeader.vue'

describe('HelloWorld', () => {
  it('renders properly', () => {
    const wrapper = mount(PageHeader, { props: { msg: 'Hello Vitest' } })
    expect(wrapper.text()).toContain('Hello Vitest')
  })
})
